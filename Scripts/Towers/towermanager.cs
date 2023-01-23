using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towermanager : MonoBehaviour {

    public float damageBoost;
    public float ProjectileSpeedBoost;

    public float weight;

    public bool NoDamage;

    public bool melee;
    public GameObject meleeEffect;

    public int accuracy;

    public float range;
    public float fireRate;
    public float speed;
    public float damage;

    public float LifeTime;

    public GameObject projectileType;

    public Transform firePoint;

    [SerializeField]private EnemyController target;

    private Animator anim;

    private GameObject game;

    public LayerMask layerMask;

    public bool canShoot = true;
    public bool flying = false;

    private Vector3 positionOg;

    public Spawner spwn;

    public bool isFlying = false;
    private GameObject flyingChild = null;

    public int level = 1;

    public int sellPrice;

    public int Stacks = 1;

    public float maxDamage;

    private Vector3 scale;
    private bool isScaling;

    public void Set(){

        positionOg = transform.position;
        maxDamage = damage;

        scale = transform.localScale;

        transform.localScale = Vector3.zero;

        StartCoroutine(AppearAnimation());

        game = GameObject.Find("GameManager");

        if (flying == false) {

            GameObject.Find("Maps").GetComponentInChildren<AudioSource>().Play();

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
            flyingChild.AddComponent<flyingTower>();
            flyingChild.GetComponent<flyingTower>().perentTower = this.gameObject;
            flyingChild.AddComponent<SpriteRenderer>();

        } else {

            if (flying == true) {

                flyingChild.transform.position = positionOg;

            }

        }

        if(game.GetComponent<Powers>().power != "vengence")
            damage = maxDamage * Mathf.Pow(2, level-1) + damageBoost;

        // Glide away
        if (GameObject.FindGameObjectWithTag("Enemy") && flying) {

            isFlying = true;

            float wind = game.GetComponent<GameManager>().windSpeed * Time.deltaTime * weight * 0.15f;

            transform.position = new Vector3(transform.position.x + wind, transform.position.y + wind, transform.position.z);

        } else if (flying && this.transform.position != positionOg){

            Vector3 ToTarget = positionOg - transform.position;
            float ang = Mathf.Atan2(ToTarget.y, ToTarget.x) * Mathf.Rad2Deg;
            Quaternion qu = Quaternion.AngleAxis(ang, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, qu, Time.deltaTime * speed * 2f);

            float step = 1.5f * Time.deltaTime;

            this.transform.position = Vector2.MoveTowards(this.transform.position, positionOg, step);

            isFlying = true;

        } else if (flying) {

            isFlying = false;

        }


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

        if (c == false)

            target = null;

        if (target != null) {

            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

            if (canShoot == true) {

                if (!melee) {

                    shoot(q);

                } else if (melee) {

                    target.Dealdamage(damage);
                    Instantiate(meleeEffect, target.transform.position, Quaternion.identity);

                    StartCoroutine(cooldown(fireRate));

                }

            }
        }

        if (!isScaling) {

            float bonusSize = 1 + (damage / maxDamage) * 0.05f;

            if (bonusSize > 1.75) {

                bonusSize = 1.75f;

            }

            transform.localScale = new Vector2(scale.x * bonusSize, scale.y * bonusSize);

        }


    }

    private IEnumerator cooldown(float time){

        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;

    }

    void shoot(Quaternion q) {

        GameObject project = Instantiate(projectileType, firePoint.position, Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed));

        if (NoDamage) {

            project.GetComponent<projectile>().damage = 0;

        } else {
        
            project.GetComponent<projectile>().damage = damage;

        }

        project.GetComponent<projectile>().lifeTime += LifeTime;
        project.GetComponent<projectile>().statusStack += Stacks;
        project.GetComponent<projectile>().speed += ProjectileSpeedBoost;


        StartCoroutine(cooldown(fireRate));

    }

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

}
