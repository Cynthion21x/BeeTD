using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate : MonoBehaviour {

    public string ability;
    public GameObject effect;
    public int Cost;

    private int ogCost;

    void Start() {

        ogCost = Cost;

    }

    void Update() {

        Cost = (int)(ogCost * GameObject.Find("GameManager").GetComponent<Shop>().abilitydiscount);

    }

    public void trigger() {

        GameObject summon = Instantiate(effect, this.transform.position, Quaternion.identity);

        if (ability == "summon") {

            summon.GetComponent<towermanager>().Set();

        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject i in enemies) {

            EnemyController en = i.GetComponentInChildren<EnemyController>();

            if (ability == "damage") {

                en.Dealdamage(100);

            }

            if (ability == "Slow") {

                en.statusEffect.Add("slow");

            }

            if (ability == "push") {

                en.statusEffect.Add("push");
                en.Dealdamage(20);

            }

        }

        if (ability == "heal") {

            GameObject.Find("GameManager").GetComponent<GameManager>().hp += 1;

         }

         if (ability == "summon") {

                Destroy(gameObject);

         }

    }

}
