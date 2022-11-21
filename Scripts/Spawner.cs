using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public string mode;

    public Image modeIcon;
    public Sprite easy;
    public Sprite tricky;

    public GameObject wasp1;
    public GameObject wasp2;
    public GameObject wasp3;
    public GameObject wasp4;

    public Transform[] positions;
    public float spacing;

    public int Wave = 1;

    public Button button;
    public TextMeshProUGUI text;

    public bool autoplay;
    public bool spawning;
    public bool flying;

    void Update(){

        text.text = (Wave-1).ToString();

        if (GameObject.FindGameObjectWithTag("Enemy")) {

            button.interactable = false;

        } else {

            button.interactable = true;

        }

        GameObject[] tow = GameObject.FindGameObjectsWithTag("bug");
        int requiredCount = tow.Length;
        int currentCount = 0;

        foreach (GameObject i in tow) {

            if (i.GetComponent<towermanager>().isFlying == false) {
                currentCount++;
            }

        }

        if (currentCount >= requiredCount) {

            flying = false;

        } else {

            flying = true;

        }

        if (button.interactable && autoplay && spawning == false && flying == false) {

            spawning = true;
            spawn();

        }

        if (mode == "tricky"){

                modeIcon.sprite = tricky;

        }

        if (mode == "easy"){

                modeIcon.sprite = easy;

        }

        if (mode == "regular") {

            modeIcon.enabled = false;;

        } else {

            modeIcon.enabled = true;

        }

    }

    public void spawn(){

        if (Wave % 16 == 0) {

            StartCoroutine(AsyncSpawn(wasp4, 0, 3000, .75f));

            Debug.Log("SpawnBoss");

            Wave++;

            return;
        }

        int spawnCount = Wave;

        mode = "easy";

        if (spawnCount >= 16) {

            spawnCount = 16;
            mode = "regular";

        }

        if (Wave >= 64) {

            mode = "tricky";

        }

        for (int i = 1; i < spawnCount+1; i++) {


            if (i % 16 == 0) {

                float QueenHp = 700;
                float QueenSpeed = .75f;

                StartCoroutine(AsyncSpawn(wasp3, (float)(i - 1) * (float)(spacing), QueenHp, QueenSpeed));

                Debug.Log("SpawnQueen");

            } else if (i % 4 == 0) {

                float SpeedHp = 70;
                float SpeedSpeed = 1.5f;

                StartCoroutine(AsyncSpawn(wasp2, (float)(i - 1) * (float)(spacing), SpeedHp, SpeedSpeed));
                Debug.Log("SpawnSpeed");

            } else {

                float DroneHp = 100;
                float DroneSpeed = 1f;

                StartCoroutine(AsyncSpawn(wasp1, (float)(i - 1) * (float)(spacing), DroneHp, DroneSpeed));
                Debug.Log("SpawnNormal");

            }

        }

        Wave++;

    }

    private IEnumerator AsyncSpawn(GameObject type, float time, float hp, float speed){

        yield return new WaitForSeconds(time);

        spawning = false;
        
        GameObject enemy = Instantiate(type, transform.position, Quaternion.identity);

        enemy.GetComponentInChildren<EnemyController>().target = positions;

        if (mode == "easy") {

            enemy.GetComponentInChildren<EnemyController>().hp = hp;

        }

        if (mode == "tricky") {

            enemy.GetComponentInChildren<EnemyController>().hp = hp * 2f * (Wave - 59);

        }

        if (mode == "regular") {

            enemy.GetComponentInChildren<EnemyController>().hp = hp * (Wave-15) * 0.10204081632f;

        }

        enemy.GetComponentInChildren<EnemyController>().speed = speed;

    }

}
