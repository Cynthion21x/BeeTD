using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class BossHp : MonoBehaviour {

    public Slider bar;
    public GameObject bossG = null;
    public GameObject bossE = null;

    public float maxHP;

    public GameObject effects;

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
                effects.GetComponent<Volume>().weight = 0;

            }

        }

        if (bossG != null || bossE != null) {
 
            bar.gameObject.SetActive(true);
            bar.maxValue = maxHP;

            effects.SetActive(true);

            bar.value = GameObject.Find("wasp").GetComponent<EnemyController>().hp;

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
