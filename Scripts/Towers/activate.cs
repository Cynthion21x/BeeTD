using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate : MonoBehaviour {

    public bool notFullscreen;

    public string ability;
    public GameObject effect;
    public int Cost;

    private int ogCost;

    public StatusEffectData status;

    void Start() {

        ogCost = Cost;

    }

    void Update() {

        Cost = (int)(ogCost * GameObject.Find("GameManager").GetComponent<Shop>().abilitydiscount);

    }

    public void trigger() {

        GameObject summon = Instantiate(effect, Vector2.zero, Quaternion.identity);

        if (notFullscreen) {

            summon.transform.position = transform.position;

        }

        if (ability == "summon") {

            summon.GetComponent<towermanager>().Set();

        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject i in enemies) {

            EnemyController en = i.GetComponentInChildren<EnemyController>();

            if (ability == "damage") {

                en.Dealdamage(100);

            }

            if (ability == "Status") {

                en.ApplyEffect(status, GetComponent<towermanager>().level);

            }

            if (ability == "push") {

                en.ApplyEffect(status, GetComponent<towermanager>().level);
                en.Dealdamage(20 * GetComponent<towermanager>().level);

            }

            if (ability == "jump") {

                this.transform.position = en.transform.position;

            }

             if (ability == "Detonate") {

                if (!en.isBoss) { Destroy(en.gameObject); }
                else { en.Dealdamage(en.hp / 6); }

             }

        }

        if (ability == "heal") {

            GameObject.Find("GameManager").GetComponent<GameManager>().hp += 1;

         }

         if (ability == "summon") {

                Destroy(gameObject);

         }

         if (ability == "Detonate") {

                
                Destroy(gameObject);

         }

    }

}
