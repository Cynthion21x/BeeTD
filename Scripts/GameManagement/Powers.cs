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

                    gameManager.bonusBaseDamage = 1;

                    selectingPower = false;

                    break;

                case "protection":

                    gameManager.hp += 5;
                    gameManager.coin += 50;

                    selectingPower = false;

                    break;

                case "creation":

                    gameManager.coin += 75;
                    selectingPower = false;

                    break;

                case "regulation":

                    gameManager.EgainRate = 2;
                    gameManager.bonusWeight = 0.05f;
                    selectingPower = false;

                    break;

            }

        }

        //continueous powers

        if (power == "vengence") {

            //On Hp change
            if (gameManager.hp != startHp) { venStats += 1; startHp = gameManager.hp; }

            GameObject[] towers = GameObject.FindGameObjectsWithTag("bug");

            //recalculate damage as vengence
            foreach (GameObject i in towers) {

                i.GetComponent<towermanager>().damage = ((i.GetComponent<towermanager>().baseDamage + gameManager.bonusBaseDamage) * Mathf.Pow(2, i.GetComponent<towermanager>().level-1)) * (1 + (0.075f * venStats)) + i.GetComponent<towermanager>().damageBoost + gameManager.bonusDamage;

            }

        }

        //Items - 1 Time use

        if (checkingItems == true) {

            foreach (string i in items) {

                GameObject[] towers = GameObject.FindGameObjectsWithTag("bug");

                if (i == "weight") {

                    gameManager.bonusWeight = 0.5f;

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "stinger") {

                    gameManager.bonusDamage += 2;

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "toy dragon") {

                    gameManager.bonusBaseDamage += 1;

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

                    gameManager.bonusSpeed += 1;

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "Prosthetic Arms") {

                    gameManager.bonusAttackSpeed = 0.1f;

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "sunglasses") {

                    gameManager.bonusRange = 0.5f;

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

                    gameManager.bonusProjectileSpeed += 0.25f;               

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "solar panel") {

                    gameManager.EgainRate += 1;             

                    items.Remove(i);
                    ogLen = items.Count;

                }

                if (i == "insect repellent") {

                    gameManager.spawner.spawnDelay += 0.5f;             

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
