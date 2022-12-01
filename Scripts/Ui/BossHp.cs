using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour {

    public Slider bar;
    public GameObject bossG = null;
    public GameObject bossE = null;

    public float maxHP;

    void Start() {

        bossG = null;
        bossE = null;

    }

    void Update() {

        if (bossG == null && bossE == null) {

            bossG = GameObject.Find("waspBoss(Clone)");
            bossE = GameObject.Find("Elite(Clone)");

            if (bossG != null || bossE != null) {

                maxHP = GameObject.Find("wasp").GetComponent<EnemyController>().hp;

            }

        }

        if (bossG != null || bossE != null) {
 
            bar.gameObject.SetActive(true);
            bar.maxValue = maxHP;

            bar.value = GameObject.Find("wasp").GetComponent<EnemyController>().hp;

        } else {

            bar.gameObject.SetActive(false);

        }
        
    }
}
