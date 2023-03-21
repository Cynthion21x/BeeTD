using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public List<System.Object[]> Data;

    [Header("Calculated Stats")]
    public float ResistiveArmour;
    public float speed;
    public float hp;
    public int coinDrop;

    [Header("Basic Stats")]
    public float maxHp;
    public int damage;
    public float baseSpeed;

    public int baseCoinDrop;

    public float RawArmour;
    public float BaseResistiveArmour;
    public float spawnImmune;

    [Header("Bonus Stats")]
    public float bonusSpeed;
    public int bonusCoins;

    [Header("Properties")]
    public Sprite image;
    public string title;

    public bool flying;
    public bool dropsItems;

    public Trigger effectTrigger;
    public string ability;

    public float deathTime;

    [Header("Track")]
    public Transform[] nodes;
    public int increment;
    public int currentNode;

    [Header("Spawnables")]
    public GameObject onDeath;
    public EnemyController enemyAhead;
    public GameObject DamagePopup;
    public GameObject miniWasp;

    [Header("Other")]
    public SpriteRenderer DamageSprite;
    public bool isHornet;
    public bool isBoss;
    public GameObject moneyEffect;

    [Header("sounds")]
    public AudioSource spawnSound;
    public AudioSource deathSound;

    private bool immune;
    private float invunTimer;
    private GameManager game;
    private bool deathing = false;

    void Start() {

        game = GameObject.Find("GameManager").GetComponent<GameManager>();

        hp = maxHp;
        speed = baseSpeed;
        coinDrop = baseCoinDrop;
        ResistiveArmour = BaseResistiveArmour;

        immune = true;
        invunTimer = spawnImmune;

        Data = new List<object[]>();

        if (effectTrigger == Trigger.spawn) { effect(); }

    }

    void Update() {

        for (int i = 0; i < Data.Count; i++) {
            Data[i][3] = (float)Data[i][3] - Time.deltaTime;

            StatusEffectData effect = (StatusEffectData)Data[i][0];
            int potent = (int)Data[i][1];

            float tick = (float)Data[i][2];
            float lifeTime = (float)Data[i][3];

            if (effect.movement != 0) { bonusSpeed = baseSpeed * -effect.movement; }
            if (effect.coinBoost != 0) { bonusCoins = effect.coinBoost * potent; }
            if (effect.armourShred != 0) { ResistiveArmour = BaseResistiveArmour - effect.armourShred; }

            if ( tick <= 0 ) {

                if (effect.percentDamage) { 
                    
                    Dealdamage((maxHp * effect.dotAmmount) * potent, true);
                
                } else { 
                    
                    Dealdamage(effect.dotAmmount * potent, true);
                
                }

                if (effect.effect != null) { Instantiate(effect.effect, transform.position, Quaternion.identity); }

                Data[i][2] = effect.tickSpeed;

            } else {

                Data[i][2] = (float)Data[i][2] - Time.deltaTime;

            }

            if (lifeTime <= 0) { Data.RemoveAt(i); bonusSpeed = 0; bonusCoins = 0; ResistiveArmour = BaseResistiveArmour; }

        }

        speed = baseSpeed + bonusSpeed;
        coinDrop = baseCoinDrop + bonusCoins;

        if (isHornet) {

            if (immune == true) {

                speed = 0f;
                Vector2 vectorToTarget = (Vector2)nodes[currentNode].position - (Vector2)transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed * 2);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);

            }

        }

        DamageSprite.color = new Color(1, 1, 1, 1 - hp / maxHp);

        if (invunTimer < 0) {

            immune = false;

        } else {

            invunTimer -= Time.deltaTime;

        }

        if (deathing) { speed = 0; }

        if (immune == false && hp <= 0) {

            //OnDeath Effects
            Death(true);

        }

        if (currentNode >= nodes.Length) {

            currentNode = nodes.Length -1;

        }


        if ((Vector2)nodes[currentNode].position != (Vector2)transform.position) {

            //move to next node

            Vector2 vectorToTarget = (Vector2)nodes[currentNode].position - (Vector2)transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed * 2);

            this.transform.position = Vector2.MoveTowards(this.transform.position, (Vector2)nodes[currentNode].position, speed * Time.deltaTime);

        } else if (currentNode >= (nodes.Length - 1)) {

            Death();
            game.hp -= damage;

        } else {

            currentNode += increment;

            //OnNode abilities
            if (effectTrigger == Trigger.corner) { effect(); }

        }

    }

    public void ApplyEffect(StatusEffectData sdata, int potency) {

        var item = new System.Object[] { sdata, potency, 0f, sdata.lifeTime };

        bool canAdd = true;

        for (int i = 0; i < Data.Count; i++) {

            StatusEffectData effect = (StatusEffectData)Data[i][0];

            if (effect.name == sdata.name) { canAdd = false; }

        }

        if (canAdd) { Data.Add(item); }
 
    }
    
    public void Dealdamage(float dmg, bool isDot = false) {

        //calculate modifyers
        float totalDamage = (dmg * 1-ResistiveArmour) - RawArmour;

        if (RawArmour > dmg) {

            totalDamage = 0;

        }

        bool crit = false;

        if (UnityEngine.Random.Range(1, 100) > 70) {

            crit = true;
            totalDamage *= 1.5f;

        }

        if (isDot) {

            crit = false;

        }

        hp -= totalDamage;

        if (totalDamage < 0) {

            hp += totalDamage;
            hp -= dmg;

        }

        GameObject num = Instantiate(DamagePopup, transform.position, Quaternion.identity);

        popup damagePopup = num.GetComponent<popup>();

        //manage popups
        if (dmg < 0) {

            damagePopup.Setup(Mathf.Abs(dmg), "heal", 7);

        } else if ((int)totalDamage == 0) {

            damagePopup.Setup(totalDamage, "none", 4);

        } else if (isDot) {

            damagePopup.Setup(totalDamage, "dot", 7);

        } else if (crit) {

            damagePopup.Setup(totalDamage, "crit", 8);

        } else if (RawArmour > 0) {

            damagePopup.Setup(totalDamage, "shield", 5);

        } else if (ResistiveArmour < 0) {

            damagePopup.Setup(totalDamage, "cricket", 7);

        } else {

            damagePopup.Setup(totalDamage, "hit", 6);

        }

    }

    public enum Trigger {

        corner,
        death,
        spawn,
        none

    }

    private void effect() {

        switch (ability) {

            case "spawn": 

                EnemyController miniWaspClone = Instantiate(miniWasp).GetComponent<EnemyController>();

                miniWaspClone.transform.position = this.transform.position;
                miniWaspClone.nodes = nodes;
                miniWaspClone.currentNode = currentNode;

                break;

            case "heal":

                enemyAhead.Dealdamage(hp * -0.5f);
                break;

            case "crazy":

                increment = UnityEngine.Random.Range(0, 3);
                break;

            case "destroy":

                var bugs = GameObject.FindGameObjectsWithTag("bug");

                foreach (GameObject b in bugs) {

                    var bT = b.GetComponent<towermanager>();

                    int cost = -(bT.GetComponent<towermanager>().sellPrice);
                    Instantiate(moneyEffect, b.transform.position, Quaternion.identity);
                    game.coin -= cost;

                    Destroy(b);

                }

                break;

        }

    }

    public void Death(bool reward = false) {

        StartCoroutine(death(reward));

    }

    public IEnumerator death(bool reward) {

        deathing = true;

        if (reward) {

            Animator a = GetComponent<Animator>();

            if (a != null) { a.SetTrigger("die"); }

        }

        yield return new WaitForSeconds(deathTime);

        if (reward) {

            if (dropsItems) { game.GetComponent<itemList>().selecting = true; }

            if (effectTrigger == Trigger.death) { effect(); }

            if (onDeath != null) { Instantiate(onDeath, transform.position, Quaternion.identity); }

            game.coin += coinDrop;

        } else {

            game.coin += (coinDrop / 2);

        }

        Destroy(gameObject);

    }
}