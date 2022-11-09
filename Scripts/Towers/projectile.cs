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

    void Start() {

        StartCoroutine(kill(lifeTime));

    }

    void Update() {

        transform.position += transform.right * Time.deltaTime * speed;

    }

    void OnCollisionEnter2D(Collision2D col) {

        EnemyController enemy = col.collider.GetComponent<EnemyController>();

        if (enemy.enabled == true) {
            enemy.hp -= damage;

            if (enemy.speed >= minSpeed)
                enemy.speed -= speedReduce;

            Instantiate(ImpactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private IEnumerator kill(float Time) {

        yield return new WaitForSeconds(Time);

        if (DespawnEffect != null){

            Instantiate(DespawnEffect, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

}
