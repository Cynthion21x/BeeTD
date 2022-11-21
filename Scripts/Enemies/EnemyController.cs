using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

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

    void Start(){

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

        foreach (string i in statusEffect) {

            if (i != "none" && clearing == false) {

                float time = 2f;

                if (i == "push") {

                    time = 0.2f;

                } if (i == "poision" || i == "damage") {

                    time = 1f;

                }

                StartCoroutine(cleanStatus(i, time));
                clearing = true;

            }

            if (i == "slow") {

                speed = maxSpd * 0.5f;


             }

             if (i == "money") {

                coinDrop = maxCost + 5;
                damage = maxDamage + 1;

              }

              if (i == "posion") {

                hp -= (maxHp * 0.0275f) * Time.deltaTime;

              }

              if (i == "burn") {

                 hp -= 10f * Time.deltaTime;

               }

               if (i == "push") {

                 speed = maxSpd * -1f;

               }

               if (i == "damage") {

                 damageMult += 1.25f * Time.deltaTime;

               }

        }

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

        //coinDrop = maxCost;
        speed = maxSpd;
        damage = maxDamage;

        damageMult = 1f;

        clearing = false;

    }

    public void Dealdamage(float dmg) {

        hp -= dmg * damageMult;

    }

}
