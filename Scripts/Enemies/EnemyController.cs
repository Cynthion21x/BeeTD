using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject DamagePopup;
    public float damageReduce;

    public float hp = 100;
    public float speed;
    public int inc;

    public GameObject deathEffect;

    public Transform[] target;
    private int current = 0;

    private bool invun = false;
    private float maxHp;
    private float maxSpd;
    private int maxCost;
    private int maxDamage;

    public int coinDrop;
    public int damage;

    public bool clearing;

    public float damageMult = 1f;

    public List<string> statusEffect;

    public SpriteRenderer damageSprite;

    public int glue;
    private float totalDot;

    void Start(){

        InvokeRepeating("DOT", 1, 1);

        maxHp = hp;
        maxSpd = speed;
        maxCost = coinDrop;
        maxDamage = damage;
        invun = true;
        StartCoroutine(cooldown());

        statusEffect = new List<string>() { "none" };

        damageSprite.color = new Color(1, 1, 1, 0);

    }

    void Update(){

        damageSprite.color = new Color(1, 1, 1, 1- hp / maxHp);

        float cricketStacks = 1;

        totalDot = 0;

        foreach (string i in statusEffect) {

            if (i != "none") {

                float time = 2f;

                if (i == "push") {

                    time = 0.2f;

                }
                
                if (i == "poision" || i == "damage") {

                    time = 5f;

                }

                if (i == "slow") {

                    time = 5f;

                }

                StartCoroutine(cleanStatus(i, time));

            }

            if (i == "slow") {

                speed = maxSpd * 0.5f;

                totalDot += glue;

             }

             if (i == "money") {

                coinDrop = maxCost + 5;
                damage = maxDamage + 1;

              }

              if (i == "posion") {

                //hp -= (maxHp * 0.005f) * Time.deltaTime;

                totalDot += (maxHp * 0.01f);

              }

              if (i == "burn") {

                totalDot += 4;

               }

               if (i == "push") {

                 speed = maxSpd * -1.5f;

               }

               if (i == "damage") {

                 cricketStacks += 0.5f;

               }

        }

        damageMult = cricketStacks;

        if (invun){
            hp = maxHp;
            speed = maxSpd;
        }

        if (hp <= 0) {
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);

            GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameManager>().coin += coinDrop;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if(current > (target.Length - 1)){
            GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameManager>().hp -= damage;
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }

        if (current >= target.Length) {

            current = target.Length -1;

        }

        if ((Vector2)transform.position != (Vector2)target[current].position) {

            Vector3 vectorToTarget = target[current].position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed * 2);

            float step = speed * Time.deltaTime;

            this.transform.position = Vector2.MoveTowards(this.transform.position, target[current].position, step); 

        } else if(current >= (target.Length - 1)) {

            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);

            GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameManager>().hp--;

        } else {

            current+=inc;

        }

        //Debug.Log(transform.position);
        //Debug.Log(target[current].position);
        //Debug.Log(current);

    }

    public IEnumerator cooldown(){

        yield return new WaitForSeconds(2f);
        invun = false;

    }

    public IEnumerator cleanStatus(string status, float time) {

        yield return new WaitForSeconds(time);

        statusEffect.Remove(status);

        coinDrop = maxCost;
        speed = maxSpd;
        damage = maxDamage;

        if (status == "damage") {

            damageMult = 1f;

        }

    }

    public void Dealdamage(float dmg, bool isDot = false) {

        float totalDamage = (dmg * damageMult) - damageReduce;

        if (damageReduce > dmg) {

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

        GameObject num = Instantiate(DamagePopup, transform.position, Quaternion.identity);

        popup damagePopup = num.GetComponent<popup>();

        if (totalDamage < 0) {

            damagePopup.Setup(totalDamage, "heal", 6);

        } else if ((int)totalDamage == 0) {

            damagePopup.Setup(totalDamage, "none", 4);

        } else if (isDot) {

            damagePopup.Setup(totalDamage, "dot", 7);

        } else if (crit) {

            damagePopup.Setup(totalDamage, "crit", 8);

        } else if (damageReduce > 0) {

            damagePopup.Setup(totalDamage, "shield", 5);

        } else if (damageMult > 1) {

            damagePopup.Setup(totalDamage, "cricket", 7);

        } else {

            damagePopup.Setup(totalDamage, "hit", 6);

        }

        }

    void DOT() {

        if (totalDot > 0) {

            Dealdamage(totalDot, true);

        }

    }

}