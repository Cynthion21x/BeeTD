using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSkin : MonoBehaviour {

    public Sprite Skin;
    public int id;

    void Start() {

        if (PlayerPrefs.GetInt("sA") == id) {

            GetComponent<SpriteRenderer>().sprite = Skin;

        }
        
    }

}
