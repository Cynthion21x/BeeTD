using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towermanager : MonoBehaviour {

    public float range;
    public float fireRate;
    public float speed;
    public float damage;
    
    public GameObject projectileType;

    public Transform firePoint;

    private EnemyController target;

    public LayerMask layerMask;

    public bool canShoot = true;
    public bool flying = false;

    private Vector3 positionOg;

    public Spawner spwn;

    public void Set(){

        positionOg = transform.position;

    }

    void Update(){


        if (GameObject.FindGameObjectWithTag("Enemy") && flying) {

            spwn.flying = true;

            float wind = GameObject.Find("GameManager").GetComponent<GameManager>().windSpeed * Time.deltaTime * 0.0075f;

            transform.position = new Vector3(transform.position.x + wind, transform.position.y + wind, transform.position.z);

        } else if (flying && this.transform.position != positionOg){

            Vector3 ToTarget = positionOg - transform.position;
            float ang = Mathf.Atan2(ToTarget.y, ToTarget.x) * Mathf.Rad2Deg;
            Quaternion qu = Quaternion.AngleAxis(ang, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, qu, Time.deltaTime * speed * 2);

            float step = speed * Time.deltaTime * .5f;

            this.transform.position = Vector2.MoveTowards(this.transform.position, positionOg, step);

            spwn.flying = true;

        } else if (flying) {

            spwn.flying = false;

        }

        if (Physics2D.OverlapCircle(transform.position, range, layerMask)) {

            target = Physics2D.OverlapCircle(transform.position, range, layerMask).GetComponent<EnemyController>();

        } else {

            target = null;

        }

        if (target != null) {

            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

            if (canShoot == true) {

                GameObject project = Instantiate(projectileType, firePoint.position, Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed));
                project.GetComponent<projectile>().damage = damage;
            
                StartCoroutine(cooldown(fireRate));

            }
        }

    }

    private IEnumerator cooldown(float time){

        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;

    }

}
