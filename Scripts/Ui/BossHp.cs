using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class BossHp : MonoBehaviour {

    public Slider bar;
    public GameObject boss = null;


    public float maxHP;

    public GameObject effects;

    void Start() {

        boss = null;

    }

    void Update() {

        /*if (boss == null) {

            boss = GameObject.Find("waspBoss(Clone)");

            if (boss != null) {

                maxHP = GameObject.Find("wasp").GetComponent<EnemyController>().hp;
                effects.GetComponent<Volume>().weight = 0;

            }

        }*/

        if (boss != null) {
 
            bar.gameObject.SetActive(true);
            bar.maxValue = boss.GetComponentInChildren<EnemyController>().maxHp;

            effects.SetActive(true);
            bar.value = boss.GetComponentInChildren<EnemyController>().hp;

            Volume vol = effects.GetComponent<Volume>();

            if (vol.weight <= 1) {

                vol.weight += Time.unscaledDeltaTime;

            }

        } else {

            bar.gameObject.SetActive(false);
            effects.SetActive(false);

        }
        
    }
}
