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

    public int coinDrop;
    public int damage;

    void Start(){

        maxHp = hp;
        maxSpd = speed;
        invun = true;
        StartCoroutine(cooldown());

    }

    void Update(){

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

}
