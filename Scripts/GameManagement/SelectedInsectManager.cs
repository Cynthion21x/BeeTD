using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectedInsectManager : MonoBehaviour {

    public int selectedInsectID;
    public string selectedPower;

    public GameObject[] teamHolders;

    public int[] party;

    void Start() {

        party = SaveSystem.LoadInt("InsectParty");
        selectedPower = SaveSystem.LoadStr("Class")[0];

    }

    public void SetPower(string name) {

        selectedPower = name;

    }

    public void SetID(int id) {

        selectedInsectID = id;

    }

    public void Save() {

        SaveSystem.Save("InsectParty", party);

        string[] powerup = { selectedPower };

        SaveSystem.Save("Class", null, powerup);

    }

}
