using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public float EnemyScale;
    public float EnemyHpBoost;

    public itemList selectingItem;

    public string mode;

    public Image modeIcon;
    public Sprite easy;
    public Sprite tricky;

    public GameObject wasp1;
    public GameObject wasp2;
    public GameObject wasp3;
    public GameObject wasp4;

    public GameObject[] elites;

    public Transform[] positions;
    public float spacing;

    public int Wave = 1;

    public Button button;
    public TextMeshProUGUI text;

    public bool autoplay;
    public bool spawning;
    public bool flying;

    public int hpBonus = 0;
    public int coinBonus = 0;

    public int glue = 0;

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

            if (selectingItem.selecting == false) {
                if (Wave != 1) {
                    //spawning = true;
                    spawn();
                }
            }

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
        spawning = true;

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        int isElite = UnityEngine.Random.Range(1, 10);

        Debug.Log("Spawning Elite" + isElite.ToString());

        if (isElite == 1 && mode != "easy") {

            float QueenHp = 500;
            float QueenSpeed = .8f;

            StartCoroutine(AsyncSpawn(elites[UnityEngine.Random.Range(1, elites.Length)], 0, QueenHp, QueenSpeed));

            Debug.Log("SpawnElite");

            return;

        }

        if (Wave % 16 == 0) {

            StartCoroutine(AsyncSpawn(wasp4, 0, 800, .75f));

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

        if (Wave >= 48) {

            mode = "tricky";

        }

        string waveType = "normal";

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        int decider = UnityEngine.Random.Range(1, 20);

        Debug.Log("Selecting Wave: " + decider.ToString());

         if (mode == "easy") {

            waveType = "normal";
            decider = 0;

        }


        if (decider == 1) {

            waveType = "flying";

            spawnCount = 19;


        }

        if (decider == 2) {

            waveType = "bossRush";

            spawnCount = 3;

        }


        if (decider == 3) {

            waveType = "normal";
            spawnCount = spawnCount + spawnCount;


        }

        for (int i = 1; i < spawnCount+1; i++) {

                if (i % 16 == 0 && waveType == "normal") {

                    float QueenHp = 300;
                    float QueenSpeed = .75f;

                    StartCoroutine(AsyncSpawn(wasp3, (float)(i - 1) * (float)(spacing), QueenHp, QueenSpeed));

                    Debug.Log("SpawnQueen");

                } else if (i % 4 == 0 && waveType == "normal") {

                    float SpeedHp = 75;
                    float SpeedSpeed = 1.5f;

                    StartCoroutine(AsyncSpawn(wasp2, (float)(i - 1) * (float)(spacing), SpeedHp, SpeedSpeed));
                    Debug.Log("SpawnSpeed");

                } else if (waveType == "normal") {

                    float DroneHp = 125;
                    float DroneSpeed = 1f;

                    StartCoroutine(AsyncSpawn(wasp1, (float)(i - 1) * (float)(spacing), DroneHp, DroneSpeed));
                    Debug.Log("SpawnNormal");

                }

                if (waveType == "bossRush") {

                    float QueenHp = 600;
                    float QueenSpeed = .75f;

                    StartCoroutine(AsyncSpawn(wasp3, (float)(i - 1) * (float)(spacing), QueenHp, QueenSpeed));

                }

                if (waveType == "flying") {

                    float SpeedHp = 125;
                    float SpeedSpeed = 1.5f;

                    StartCoroutine(AsyncSpawn(wasp2, (float)(i - 1) * (float)(spacing), SpeedHp, SpeedSpeed));                 

                }

            }

        Wave++;

    }

    private IEnumerator AsyncSpawn(GameObject type, float time, float hp, float speed){

        yield return new WaitForSeconds(time);
        
        GameObject enemy = Instantiate(type, positions[0].position, Quaternion.identity);

        enemy.GetComponentInChildren<EnemyController>().target = positions;

        if (mode == "easy") {

            enemy.GetComponentInChildren<EnemyController>().hp = hp;

        }

        if (mode == "tricky") {

            enemy.GetComponentInChildren<EnemyController>().hp = hp * (1 + ((Wave - 32) * EnemyScale * 1.5f));

        }

        if (mode == "regular") {

            enemy.GetComponentInChildren<EnemyController>().hp = hp * (1 + ((Wave-15) * EnemyScale));

        }

        enemy.GetComponentInChildren<EnemyController>().speed = speed;
        enemy.GetComponentInChildren<EnemyController>().coinDrop += coinBonus;

        spawning = false;
    }

}
