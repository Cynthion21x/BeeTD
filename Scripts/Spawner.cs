using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public enum Mode {

        easy,
        normal,
        tricky

    }

    [Header("options")]
    public bool autoplay;
    public itemList items;
    public float spawnDelay;

    [Header("bonus")]
    public int coinBonus;
    public int glue;

    [Header("UI")]
    public BossHp hpBar;
    public Image modeIcon;

    public Sprite easyMode;
    public Sprite hardMode;

    public TextMeshProUGUI text;
    public Button button;

    public GameObject waveUI;

    [Header("Map")]
    public mapSelector mapSelector;
    public map map;

    [Header("Waves")]
    public int wave;

    public int bossWave;
    public int hornetWave;

    public Mode mode;
    public int EasyMode;
    public int NormalMode;
    public int TrickyMode;

    public int flyingCount;

    [Header("Spawn List")]
    public List<GameObject> EnemiesToSpawn;

    [Header("Enemies")]
    public GameObject[] groundWasps;
    public GameObject[] airWasps;
    public GameObject[] eliteWasps;

    public GameObject[] bosses;

    public GameObject hornet;

    private bool spawning;
    private EnemyController previouslySpawned;
    private bool isBoss;
    private float spawnTimer;

    void Start() {

        spawnDelay = 2;
        generateSpawnList();

    }

    void Update() {

        map = mapSelector.mapSelected;
        text.text = (wave - 1).ToString();

        if (wave >= EasyMode) { mode = Mode.easy; modeIcon.sprite = easyMode; modeIcon.color = new Color(1, 1, 1, 1); }
        if (wave >= NormalMode) { mode = Mode.normal; modeIcon.color = new Color(0, 0, 0, 0); }
        if (wave >= TrickyMode) { mode = Mode.tricky; modeIcon.sprite = hardMode; modeIcon.color = new Color(1, 1, 1, 1); }

        if (spawning) {

            if (EnemiesToSpawn.Count > 0) {

                if (spawnTimer > 0) { spawnTimer -= Time.deltaTime; } else {

                    spawnEnemy(isBoss);
                    spawnTimer = spawnDelay;

                }

            } else {

                spawning = false;
                generateSpawnList();

            }

        }

    }
    
    public void Spawn() {

        wave++;
        spawning = true;


    }

    void generateSpawnList() {

        isBoss = false;

        foreach (Transform x in waveUI.transform) {
            Destroy(x.gameObject);
        }

        int len = bossWave;
        if (mode == Mode.easy) { len = wave; };

        EnemiesToSpawn = new List<GameObject>();

        if (wave % hornetWave == 0) {

            int w = UnityEngine.Random.Range(0, bosses.Length);

            EnemiesToSpawn.Add(hornet);
            GameObject icon = new GameObject("WaspIcon", typeof(Image));
            icon.GetComponent<Image>().sprite = hornet.GetComponent<EnemyController>().image;
            icon.GetComponent<Image>().sprite = hornet.GetComponent<EnemyController>().image;
            icon.GetComponent<Image>().preserveAspect = true;
            icon.transform.parent = waveUI.transform;

            isBoss = true;
            return;

        }

        if (wave % bossWave == 0) {
            int w = UnityEngine.Random.Range(0, bosses.Length);

            EnemiesToSpawn.Add(bosses[w]);
            GameObject icon = new GameObject("WaspIcon", typeof(Image));
            icon.GetComponent<Image>().sprite = bosses[w].GetComponent<EnemyController>().image;
            icon.GetComponent<Image>().preserveAspect = true;
            icon.transform.parent = waveUI.transform;

            isBoss = true;
            return;

        }

        for (int i = 1; i < len+1; i++) {

            if (mode == Mode.easy) {

                //Add Defaults
                if (i % bossWave == 0) {

                    EnemiesToSpawn.Add(eliteWasps[0]);

                } else if (i % (bossWave / flyingCount) == 0) { EnemiesToSpawn.Add(airWasps[0]); } else { EnemiesToSpawn.Add(groundWasps[0]); }

            } else if (mode == Mode.normal) {

                if (UnityEngine.Random.Range(0, 100) >= 60) {

                    //Add Elite
                    if (i % bossWave == 0) {

                        EnemiesToSpawn.Add(eliteWasps[UnityEngine.Random.Range(0, eliteWasps.Length)]);

                    } else if (i % (bossWave / flyingCount) == 0) { 
                        
                        EnemiesToSpawn.Add(airWasps[UnityEngine.Random.Range(0, airWasps.Length)]); 
                    
                    } else { 
                        
                        EnemiesToSpawn.Add(groundWasps[UnityEngine.Random.Range(0, groundWasps.Length)]);
                    
                    }

                } else {

                    if (i % bossWave == 0) {

                        EnemiesToSpawn.Add(eliteWasps[0]);

                    } else if (i % (bossWave / flyingCount) == 0) { EnemiesToSpawn.Add(airWasps[0]); } else { EnemiesToSpawn.Add(groundWasps[0]); }

                }

            } else if (mode == Mode.tricky) {

                //Add Elite
                if (i % bossWave == 0) {

                    EnemiesToSpawn.Add(eliteWasps[UnityEngine.Random.Range(0, eliteWasps.Length)]);

                } else if (i % (bossWave / flyingCount) == 0) {

                    EnemiesToSpawn.Add(airWasps[UnityEngine.Random.Range(0, airWasps.Length)]);

                } else {

                    EnemiesToSpawn.Add(groundWasps[UnityEngine.Random.Range(0, groundWasps.Length)]);

                }

            }

        }

        foreach (var i in EnemiesToSpawn) {

            GameObject icon = new GameObject("WaspIcon", typeof(Image));
            Image img = icon.GetComponent<Image>();

            img.sprite = i.GetComponent<EnemyController>().image;
            img.preserveAspect = true;

            icon.transform.parent = waveUI.transform;

        }

    }

    void LateUpdate(){
        
        if (GameObject.FindGameObjectWithTag("Enemy") || spawning || items.selecting || wave == 1) {

            button.interactable = false;

        } else {

            button.interactable = true;

            if (autoplay) { Spawn(); }

        }

        if (wave == 1) {

            button.interactable = true;

        }

    }

    void spawnEnemy(bool boss = false) {

        GameObject enemy = Instantiate(EnemiesToSpawn[0], map.mapCorners[0]);
        EnemiesToSpawn.RemoveAt(0);

        EnemyController enemyControl = enemy.GetComponent<EnemyController>();

        enemyControl.maxHp -= glue * 10;

        if (mode == Mode.normal) { enemyControl.maxHp = enemyControl.maxHp * 0.5f * (wave - bossWave + 1); }
        if (mode == Mode.tricky) { enemyControl.maxHp = (enemyControl.maxHp * 0.4f * (wave - bossWave + 1)) * Mathf.Pow(1.01f, wave); }

        enemyControl.baseCoinDrop += coinBonus;
        enemyControl.enemyAhead = previouslySpawned;
        enemyControl.nodes = map.mapCorners;
        previouslySpawned = enemyControl;

        if (boss) {

            hpBar.boss = enemy;

        }

    }
}