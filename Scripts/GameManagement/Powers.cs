using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour {

    private bool selectingPower = true;

    public string power = "no";

    public Shop shop;
    public Spawner spawner;
    public GameManager gameManager;
    public DebugController debugging;

    private int startHp;
    private int venStats;

    public List<string> items;

    public bool selectingItem;

    private int ogLen;
    private bool checkingItems = false;

    void Start() {

        ogLen = 0;

        startHp = 10;

    }

    void Update() {

        if (selectingPower) {

            switch (power) {

                case "sandbox":

                    debugging.sandboxing = true;

                    selectingPower = false;

                    break;

                case "vengence":

                    selectingPower = false;

                    break;

                case "prosparity":

                    spawner.coinBonus = 5;
                    gameManager.coin += 25;

                    selectingPower = false;

                    break;

                case "destruction":

                    shop.baseDamageBoost = 1;

                    selectingPower = false;

                    break;

                case "protection":

                    gameManager.hp += 5;
                    gameManager.coin += 50;

                    selectingPower = false;

                    break;

                case "creation":

                    selectingPower = false;

                    break;

            }

        }

        //continueous powers

        if (power == "vengence") {

            if (gameManager.hp != startHp) {

                venStats += 1;

                startHp = gameManager.hp;

            }

            GameObject[] towers = GameObject.FindGameObjectsWithTag("bug");

            foreach (GameObject i in towers) {

                i.GetComponent<towermanager>().damage = (i.GetComponent<towermanager>().maxDamage * Mathf.Pow(2, i.GetComponent<towermanager>().level-1)) * (1 + (0.075f * venStats)) + i.GetComponent<towermanager>().damageBoost;

            }

        }

        //Items - 1 Time use

        if (checkingItems == true) {

            foreach (string i in items) {

                GameObject[] towers = GameObject.FindGameObjectsWithTag("bug");

                if (i == "weight") {

                    foreach (GameObject t in towers) {

                        t.GetComponent<towermanager>().weight *= 0.5f;
                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "stinger") {

                    shop.damageBonus += 2;

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        if (towerman.maxDamage != 0) {
                            towerman.damageBoost += 2;
                        }

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "toy dragon") {

                    shop.baseDamageBoost += 1;

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        if (towerman.maxDamage != 0) {
                            towerman.maxDamage += 1;
                        }

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "leaf") {

                    gameManager.hp += 1;
                    ogLen = items.Count;

                }

                if (i == "wallet") {

                    gameManager.coin += 50;
                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "tophat") {

                    spawner.coinBonus += 5;
                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "honey") {

                    gameManager.hp += 1;
                    gameManager.coin += 10;
                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "glue") {

                    spawner.glue++;
                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "Prosthetic Legs") {

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        towerman.speed += 1;

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "Prosthetic Arms") {

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        towerman.fireRate -= .1f;

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "sunglasses") {

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        towerman.range += 0.5f;

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "boost") {

                    for(int x = 0; x < 3; x++) {

                        GameObject t = towers[Random.Range(0, towers.Length)];

                        towermanager towerman = t.GetComponent<towermanager>();

                        gameManager.GetComponent<Shop>().FreeUpgrade(towerman);

                        Debug.Log("boosted");
                    }

                    Debug.Log("Finished Boost");

                    items.Remove("boost");
                    ogLen = items.Count;

                }

                if (i == "battery") {

                    gameManager.Ecap += 25;

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "discount card") {

                    shop.discount += 5;

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "gunpowder") {

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        towerman.ProjectileSpeedBoost += 0.5f;

                    }                    

                    items.Remove(i);
                    ogLen = items.Count;

                }

                checkingItems = false;


            }

        }

        if (ogLen != items.Count) {

            checkingItems = true;

        }

    }

    public void selectPower(string pow) {

        power = pow;

    }


}
