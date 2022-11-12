using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class projectile : MonoBehaviour {

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

                enemy.hp -= damage;

                if (!enemy.statusEffect.Contains(statusEffect))
                    enemy.statusEffect.Add(statusEffect);

                if (enemy.speed >= minSpeed)
                    enemy.speed -= speedReduce;

                Collider2D[] enemys = Physics2D.OverlapCircleAll(this.transform.position, aoe, EnemyLayer);

                foreach (Collider2D coll in enemys) {

                    if (coll.GetComponent<EnemyController>() != enemy){

                        coll.GetComponent<EnemyController>().hp -= damage * 0.75f; 
                        Debug.Log("Aditional target hit");  

                    }   

                }

            }

            Instantiate(ImpactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private IEnumerator kill(float Time) {

        yield return new WaitForSeconds(Time);

        Collider2D[] enemys = Physics2D.OverlapCircleAll(this.transform.position, aoe, EnemyLayer);

        Debug.Log("Aoe Hit: " + enemys.Length.ToString());

        foreach (Collider2D coll in enemys) {

            coll.GetComponent<EnemyController>().hp -= damage * 0.75f; 
            Debug.Log("Aditional target hit");  

        }   

        if (DespawnEffect != null){

            Instantiate(DespawnEffect, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

}
