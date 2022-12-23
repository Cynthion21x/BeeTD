using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerDesc : MonoBehaviour {

    public SelectedInsectManager selected;

    public string powerName;

    public GameObject text;

    void Update() {

        if (selected.selectedPower == powerName) {

            text.SetActive(true);

        } else {

            text.SetActive(true);

        }

    }

}
