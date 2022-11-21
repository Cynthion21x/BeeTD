using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectLoader : MonoBehaviour {

    public GameObject[] insects;
    public GameObject[] insectsText;

    public int slectedTower;

    void Start() {

        slectedTower = Random.Range(0, insects.Length);

    }

    void Update() {

        if (slectedTower >= insects.Length) {

            slectedTower = 0;

        }

        if (slectedTower <= -1) {

            slectedTower = insects.Length-1;

        }

        foreach (GameObject i in insects) {

            if (insects[slectedTower] != i) {
                
                i.SetActive(false);

            }

        }

        insects[slectedTower].SetActive(true);

        foreach (GameObject i in insectsText) {

            if (insectsText[slectedTower] != i) {
                
                i.SetActive(false);

            }

        }

        insectsText[slectedTower].SetActive(true);

    }

    public void nextTower() {

        slectedTower++;

    }

    public void PrevTower() {

        slectedTower--;


    }

}
