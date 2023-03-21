using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class projectile : MonoBehaviour {

    [Header("Behaivours")]
    public float speed;
    public float lifeTime;
    public float aoe = 0f;

    public StatusEffectData status;

    [Header("effects")]
    public GameObject ImpactEffect;

    [Header("TowerManaged")]
    public float damage;
    public int level;

    public LayerMask EnemyLayer;

    void Start() {

        //StartCoroutine(kill(lifeTime));
        Invoke("kill", lifeTime);

    }

    void Update() {

        transform.position += transform.right * Time.deltaTime * speed;

    }

    void OnCollisionEnter2D(Collision2D col) {

        EnemyController enemy = col.collider.GetComponent<EnemyController>();

        if (enemy != null){

            if (enemy.enabled == true) {

                Attack(enemy);

            }

        }

        kill();

    }

    private void kill() {

        Collider2D[] enemys = Physics2D.OverlapCircleAll(this.transform.position, aoe, EnemyLayer);

        foreach (Collider2D coll in enemys) {

            Attack(coll.GetComponent<EnemyController>(), true);

        }   

        if (ImpactEffect != null){

            Instantiate(ImpactEffect, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

    private void Attack(EnemyController target, bool aoe = false) {

        //damage
        target.ApplyEffect(status, level);

        if (aoe == true) { target.Dealdamage(damage * 0.25f); return; } 

        target.Dealdamage(damage);

    }

}
