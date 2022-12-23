using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveData {

    public int[] party;

    public string[] achievements;

    public saveData(int[] party, string[] achievements) {

        party = party;
        achievements = achievements;

    }

}
