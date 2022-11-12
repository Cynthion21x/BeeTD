using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour {

    public Slider bar;
    public GameObject bossG = null;

    public float maxHP;

    void Start() {

        bossG = null;

    }

    void Update() {

        if (bossG == null) {

            bossG = GameObject.Find("waspBoss(Clone)");

            if (bossG != null) {

                maxHP = GameObject.Find("wasp").GetComponent<EnemyController>().hp;

            }

        }

        if (bossG != null) {
 
            bar.gameObject.SetActive(true);
            bar.maxValue = maxHP;

            bar.value = GameObject.Find("wasp").GetComponent<EnemyController>().hp;

        } else {

            bar.gameObject.SetActive(false);

        }
        
    }
}
