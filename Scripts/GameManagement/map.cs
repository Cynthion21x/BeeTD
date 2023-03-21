using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class map : MonoBehaviour {

    [Header("Positions")]
    public Transform[] mapCorners;

    [Header("Properties")]
    public bool isActive;
    public bool alternatingWind;
    public bool hasWeather;

    public string mapName;
    public string mapAuthor;

    [Header("UI")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI author;


    void Update() {

        if (isActive) {

            //spawn.positions = mapCorners;

            title.text = "< " + mapName + " >";
            author.text = "by " + mapAuthor;

        }

    }

    public void Fade() {

        Destroy(gameObject);

    }

}
