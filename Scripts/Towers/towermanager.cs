using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class towermanager : MonoBehaviour {

    [Header("Calculated Stats")]
    public float damage;
    public int sellPrice;
    public float range;
    public float fireRate;
    public float speed;
    public float weight;
    public int level = 1;

    [Header("Base Stats")]
    public float baseWeight;
    public float baseRange;
    public float baseFireRate;
    public float baseSpeed;
    public float baseDamage;

    public float baseProjectileSpeed;

    [Header("Properties")]
    public bool flying = false;
    public bool NoDamage;
    public StatusEffectData Status;

    public bool melee;
    public GameObject meleeEffect;

    public bool canShoot = true;

    [Header("Projectile")]
    public GameObject projectileType;
    public Transform firePoint;

    [Header("Bonuses")]
    public float damageBoost;
    public float ProjectileSpeedBoost;
    public float ProjectileLifeTime;

    [Header("Other")]
    public Spawner spwn;

    public LayerMask layerMask;

    public bool isFlying = false;
    public GameObject flyingChild = null;


    //private
    private Vector3 scale;
    private bool isScaling;
    public bool blown;
    private GameObject game;
    private EnemyController target;
    private Vector3 positionOg;

    void Awake()  {
        range = baseRange;
    }

    public void Set(){

        positionOg = transform.position;

        scale = transform.localScale;

        transform.localScale = Vector3.zero;

        StartCoroutine(AppearAnimation());

        game = GameObject.Find("GameManager");

        if (flying == false) {

            GameObject.Find("Maps").GetComponent<mapSelector>().mapSelected.GetComponent<AudioSource>().Play();

        }

        onPlaceEffect onP = GetComponent<onPlaceEffect>();

        if (onP != null) {

            onP.Activate(game.GetComponent<GameManager>());

        }

    }

    void Update(){

        // No fly stack
        if (flyingChild == null && flying) {

            flyingChild = new GameObject();
            flyingChild.AddComponent<flyingTower>().perentTower = this.gameObject;
            flyingChild.AddComponent<SpriteRenderer>();

        } else {

            if (flying == true) {

                flyingChild.transform.position = positionOg;

            }

        }

        GameManager gameMan = game.GetComponent<GameManager>();

        //Calculate Damage
        if (game.GetComponent<Powers>().power != "vengence")
            damage = (baseDamage + gameMan.bonusBaseDamage) * Mathf.Pow(2, level-1) + damageBoost + gameMan.bonusDamage;

        //OtherStats
        range = baseRange + gameMan.bonusRange;
        fireRate = baseFireRate - gameMan.bonusAttackSpeed;
        speed = baseSpeed + gameMan.bonusSpeed;
        weight = baseWeight - gameMan.bonusWeight;

        ProjectileSpeedBoost = baseProjectileSpeed + gameMan.bonusSpeed;

        // Fly back to where your supposed to be
        if (flying && this.transform.position != positionOg && blown == false){

            Vector3 ToTarget = positionOg - transform.position;
            float ang = Mathf.Atan2(ToTarget.y, ToTarget.x) * Mathf.Rad2Deg;
            Quaternion qu = Quaternion.AngleAxis(ang, Vector3.forward);

            float step = speed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, qu, step * 2f);

            this.transform.position = Vector2.MoveTowards(this.transform.position, positionOg, step / 2);

            isFlying = true;

        } else if (flying) {

            isFlying = false;

        }

        // Select who to shoot
        if (target == null) {

            if (Physics2D.OverlapCircle(transform.position, range, layerMask)) {

                target = Physics2D.OverlapCircle(transform.position, range, layerMask).GetComponent<EnemyController>();

            }

        }

        bool c = false;

        foreach (Collider2D i in Physics2D.OverlapCircleAll(transform.position, range, layerMask)) {

            if (i.GetComponent<EnemyController>() == target) {

                c = true;

            }

        }

        if (c == false) { target = null; }

        //Found someone
        if (target != null) {

            //Rotate
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

            // Actually attack
            if (canShoot == true) {

                if (!melee) {

                    shoot(q);

                } else if (melee && canShoot) {

                    Invoke("bite", 0.1f);
                    StartCoroutine(cooldown(fireRate));

                }

            }
        }

        //Resize
        if (!isScaling) {

            float bonusSize = 1 + (damage / baseDamage) * 0.05f;

            if (NoDamage) { bonusSize = 1; }

            if (bonusSize > 1.75) { bonusSize = 1.75f; }
            
            if (bonusSize < 1) { bonusSize = 1; }

            if (float.IsNaN(bonusSize)) { bonusSize = 1; }

            transform.localScale = new Vector2(scale.x * bonusSize, scale.y * bonusSize);

        }


    }

    private IEnumerator cooldown(float time){

        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;

    }

    //Pew Pew
    void shoot(Quaternion q) {

        GameObject project = Instantiate(projectileType, firePoint.position, Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed));
        projectile projectileStats = project.GetComponent<projectile>();

        if (NoDamage) {

            projectileStats.damage = 0;

        } else {

            projectileStats.damage = damage;

        }

        projectileStats.lifeTime += ProjectileLifeTime;
        projectileStats.speed += ProjectileSpeedBoost;
        projectileStats.level = level;
        projectileStats.status = Status;

        StartCoroutine(cooldown(fireRate));

    }

    //yum
    void bite() {

        if (target != null) {

            Instantiate(meleeEffect, target.transform.position, Quaternion.identity);
            target.Dealdamage(damage);

            if (Status != null) { target.ApplyEffect(Status, level); }

        }


    }

    //squash + stretch
    public IEnumerator AppearAnimation() {

        float duration = 0.2f;

        if (isScaling)  {
            yield break;
        }

        isScaling = true;

        float counter = 0;

        Vector3 startScaleSize = transform.localScale;

        while (counter < duration) {

            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScaleSize, scale * 1.4f, counter / duration);
            yield return null;

        }

        counter = 0;

        startScaleSize = transform.localScale;

        while (counter < duration) {

            counter += Time.deltaTime;

            transform.localScale = Vector3.Lerp(startScaleSize, scale, counter / duration);

            yield return null;

        }

        isScaling = false;

    }

    //Windy
    public void Blow() {

        if(GameObject.FindGameObjectWithTag("Enemy")) {

            blown = true;
            float wind = game.GetComponent<GameManager>().windSpeed * Time.deltaTime * weight * UnityEngine.Random.Range(1, 3);
            StartCoroutine(windBlow(wind));
            
        }

    }

    private IEnumerator windBlow(float wind) {

        for (float i = 0; i <= 2; i += Time.unscaledDeltaTime) {

            transform.position += new Vector3(wind, wind);

            yield return null;
        }

        blown = false;

    }


}
