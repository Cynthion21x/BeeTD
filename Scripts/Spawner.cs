using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public GameObject wasp1;
    public GameObject wasp2;
    public GameObject wasp3;
    public GameObject wasp4;

    public Transform[] positions;
    public float spacing;

    public int Wave = 1;

    public Button button;
    public TextMeshProUGUI text;

    void Update(){

        text.text = (Wave - 1).ToString();

        if (GameObject.FindGameObjectWithTag("Enemy")) {

            button.interactable = false;

        } else {

            button.interactable = true;

        }

    }

    public void spawn(){

        if (Wave % 64 == 0) {

            float HpPow = 1;
            float SpdPow = 1;

            StartCoroutine(AsyncSpawn(wasp4, 0, HpPow, SpdPow));

            Debug.Log("SpawnBoss");

            Wave++;

            return;
        }

        int spawnCount = Wave;

        for (int i = 1; i < spawnCount+1; i++) {


            if (i % 16 == 0) {

                float QueenHp = 500 * (1f + ((float)Wave / 16));
                float QueenSpeed = 1f;

                StartCoroutine(AsyncSpawn(wasp3, (float)(i - 1) * (float)(spacing), QueenHp, QueenSpeed));

                Debug.Log("SpawnQueen");

            } else if (i % 4 == 0) {

                float SpeedHp = 50 * (1 + ((float)Wave / 16) * 2);
                float SpeedSpeed = 1f;

                StartCoroutine(AsyncSpawn(wasp2, (float)(i - 1) * (float)(spacing), SpeedHp, SpeedSpeed));
                Debug.Log("SpawnSpeed");

            } else {

                float DroneHp = 100 * (1 + ((float)Wave / 16) * 5f);
                float DroneSpeed = 1f;

                StartCoroutine(AsyncSpawn(wasp1, (float)(i - 1) * (float)(spacing), DroneHp, DroneSpeed));
                Debug.Log("SpawnNormal");

            }

        }

        Wave++;

    }

    private IEnumerator AsyncSpawn(GameObject type, float time, float hpMult, float speedMult){

        yield return new WaitForSeconds(time);
        
        GameObject enemy = Instantiate(type, transform.position, Quaternion.identity);

        enemy.GetComponentInChildren<EnemyController>().target = positions;
        enemy.GetComponentInChildren<EnemyController>().hp = hpMult;
        enemy.GetComponentInChildren<EnemyController>().speed = speedMult;

    }

}
