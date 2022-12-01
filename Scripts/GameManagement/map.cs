using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class map : MonoBehaviour {

    public Transform[] mapCorners;
    public Spawner spawn;

    public bool isActive;

    public string mapName;
    public string mapAuthor;

    public TextMeshProUGUI title;
    public TextMeshProUGUI author;

    void Update() {

        if (isActive) {

            spawn.positions = mapCorners;

            title.text = "< " + mapName + " >";
            author.text = "by " + mapAuthor;

        }

    }


}
