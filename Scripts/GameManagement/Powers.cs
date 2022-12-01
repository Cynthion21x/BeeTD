using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour {

    private bool selectingPower = true;

    public string power = "no";

    public Shop shop;
    public Spawner spawner;
    public GameManager gameManager;

    private int startHp;
    private int venStats;

    public List<string> items;

    public bool selectingItem;

    private int ogLen;

    void Start() {

        ogLen = 0;

        startHp = 10;

    }

    void Update() {

        if (selectingPower) {

            switch (power) {

                case "vengence":

                    selectingPower = false;

                    break;

                case "prosparity":

                    spawner.coinBonus = 7;

                    selectingPower = false;

                    break;

                case "destruction":

                    shop.damageBonus = 5;

                    selectingPower = false;

                    break;

                case "protection":

                    gameManager.hp += 5;
                    gameManager.coin += 75;

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

                i.GetComponent<towermanager>().damage = (i.GetComponent<towermanager>().maxDamage * Mathf.Pow(2, i.GetComponent<towermanager>().level-1)) * (1 + (0.05f * venStats));

            }

        }

        //Items - 1 Time use

        if (ogLen != items.Count) {

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
                            towerman.maxDamage += 2;
                        }

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "leaf") {

                    gameManager.hp += 1;
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

                if (i == "sunglasses") {

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        towerman.range += 0.5f;

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "boost") {

                    foreach (GameObject t in towers) {

                        towermanager towerman = t.GetComponent<towermanager>();

                        towerman.level ++;

                    }

                    items.Remove(i);
                    ogLen = items.Count;

                }

            }

        }

    }

    public void selectPower(string pow) {

        power = pow;

    }


}
