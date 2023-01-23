using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achievementLoader : MonoBehaviour {

    public int[] achievements;

    void Awake() {

        achievements = SaveSystem.LoadInt("achievements");

    }

}
