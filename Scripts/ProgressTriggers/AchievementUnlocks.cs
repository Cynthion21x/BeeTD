using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementUnlocks : MonoBehaviour {

    public enum conditionPrimary {

        hp,
        money,
        wave,
        sellValue,
        energy,
        towerCount,
        onlyTower

    }

    public enum conditionSecondary {

        onBossWave,
        greaterThan,
        withTower

    }

    public int[] id;
    public int[] value;
    public string[] towerNames;
    public conditionPrimary[] primary;
    public conditionSecondary[] secondary;

    public int[] currentAchievements;
    public GameManager gameManager;
    public Spawner spawner;

    [SerializeField] private int var;

    void Start() {

        currentAchievements = SaveSystem.LoadInt("achievements");

    }

    void Update() {

        //Debug.Log(id.Length);

        if (gameManager.GetComponent<Powers>().power == "sandbox") {

            return;

        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null) {

            return;

        }

        for (int i = 0; i < id.Length; i++) {

            if (currentAchievements.Contains(i) == false) {

                var = 0;

                switch (primary[i]) {

                    case conditionPrimary.hp:

                        var = gameManager.hp;
                        break;

                    case conditionPrimary.money:

                        var = gameManager.coin;
                        break;

                    case conditionPrimary.energy:

                        var = gameManager.energy;
                        break;

                    case conditionPrimary.wave:

                        //var = spawner.Wave;
                        break;

                    case conditionPrimary.towerCount:

                        GameObject[] towers = GameObject.FindGameObjectsWithTag("bug");
                        var = towers.Length;
                        break;

                    case conditionPrimary.sellValue:

                        int sellValue = 0;

                        GameObject[] towersS = GameObject.FindGameObjectsWithTag("bug");
                    
                        foreach(GameObject o in towersS) {

                            var p = o.GetComponent<towermanager>().sellPrice;

                            if (p > sellValue)
                                sellValue = p;

                        }

                        var = sellValue;
                        break;

                    case conditionPrimary.onlyTower:

                       GameObject[] towersO = GameObject.FindGameObjectsWithTag("bug");

                        int value = -1;

                        foreach (GameObject o in towersO) {

                            if (o.name != towerNames[i]+"(Clone)") {

                                value = 0;
                                break;

                            } else {

                                value = 1;
                                break;

                            }

                        }

                        var = value;
                        break;

                }

                if (secondary[i] == conditionSecondary.greaterThan) {

                    if (var > value[i]) {

                        Unlock(id[i]);

                    }

                }

                if (secondary[i] == conditionSecondary.withTower) {

                    GameObject[] towers = GameObject.FindGameObjectsWithTag("bug");

                    bool condition = true;

                    foreach (GameObject o in towers) {

                        if (o.name != towerNames[i]+"(Clone)") {
                            condition = false;
                            break;
                        }

                    }

                    if (var == value[i] && condition) {

                        Unlock(id[i]);

                    }

                }

                if (secondary[i] == conditionSecondary.onBossWave) {

                    if (var == value[i]) {

                        /*if (spawner.Wave % 16 == 0)

                        Unlock(id[i]);*/

                    }

                }

            }

        }
        
    }

    public void Unlock(int id) {

        List<int> a = currentAchievements.ToList();

        a.Add(id);

        currentAchievements = a.ToArray();

        Debug.Log("Unlocked Achievement: " + id.ToString());

    }

    void OnApplicationQuit() {

        Save();

    }

    public void Save() {

        SaveSystem.Save("achievements", currentAchievements);

    }

}
