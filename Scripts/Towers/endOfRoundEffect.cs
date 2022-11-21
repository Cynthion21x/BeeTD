using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endOfRoundEffect : MonoBehaviour {

    public string effect = "money";
    public int value;

    public float Escaler;

    public int BaseGain;

    private Spawner spawn;
    private towermanager tower;

    private bool canActivate = true;

    public GameObject Peffect;

    void Start() {

        value = 1;
        tower = this.GetComponent<towermanager>();
        spawn = tower.spwn;

    }

    void Update() {

        if (canActivate == true & !GameObject.FindGameObjectWithTag("Enemy") & tower.canShoot) {

            canActivate = false;

            if (effect == "money") {

                GameObject.Find("GameManager").GetComponent<GameManager>().coin += BaseGain * value;

            }

            if (Peffect != null)
                Instantiate(Peffect, transform.position, Quaternion.identity, gameObject.transform).transform.localScale = new Vector2(Escaler, Escaler);

        }

        if (GameObject.FindGameObjectWithTag("Enemy")) {

            canActivate = true;

        }

    }

}
