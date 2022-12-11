using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class projectile : MonoBehaviour {

    public int statusStack = 0;

    public float damage;
    public float speed;
    public float lifeTime;

    public float aoe = 0f;

    public float minSpeed;

    public float speedReduce;

    public GameObject ImpactEffect;
    public GameObject DespawnEffect;

    public LayerMask EnemyLayer;

    public string statusEffect = "none";

    void Start() {

        StartCoroutine(kill(lifeTime));

    }

    void Update() {

        transform.position += transform.right * Time.deltaTime * speed;

    }

    void OnCollisionEnter2D(Collision2D col) {

        EnemyController enemy = col.collider.GetComponent<EnemyController>();

        if (enemy != null){

            if (enemy.enabled == true) {

                enemy.Dealdamage(damage);

                int sCount = 0;

                foreach (string i in enemy.statusEffect) {

                    if (i == statusEffect) {

                        sCount++;

                    }

                }

                Debug.Log(sCount);
                Debug.Log(statusStack);

                if (!(sCount >= statusStack-1)) {

                    enemy.statusEffect.Add(statusEffect);

                }

                Collider2D[] enemys = Physics2D.OverlapCircleAll(this.transform.position, aoe, EnemyLayer);

                foreach (Collider2D coll in enemys) {

                    if (coll.GetComponent<EnemyController>() != enemy){

                        coll.GetComponent<EnemyController>().Dealdamage(damage * 0.75f); 
                        Debug.Log("Aditional target hit");  

                    }

                }

            }

            if (ImpactEffect != null) {

                Instantiate(ImpactEffect, transform.position, Quaternion.identity);

            }

            Destroy(gameObject);

        }

    }

    private IEnumerator kill(float Time) {

        yield return new WaitForSeconds(Time);

        Collider2D[] enemys = Physics2D.OverlapCircleAll(this.transform.position, aoe, EnemyLayer);

        Debug.Log("Aoe Hit: " + enemys.Length.ToString());

        foreach (Collider2D coll in enemys) {

            coll.GetComponent<EnemyController>().Dealdamage(damage * 0.75f); 
            Debug.Log("Aditional target hit");  

        }   

        if (DespawnEffect != null){

            Instantiate(DespawnEffect, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

}
