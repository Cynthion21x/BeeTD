using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mapSelector : MonoBehaviour {

    public GameObject[] maps;
    public bool weatherOn;

    public map mapSelected;

    [Header("UI")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI author;


    void Start() {

        loadMap();

    }


    void loadMap() {

        if (mapSelected != null) {

            mapSelected.Fade();
                
        }

        mapSelected = Instantiate(maps[Random.Range(0, maps.Length)]).GetComponent<map>();

        mapSelected.title = title;
        mapSelected.author = author;
        mapSelected.isActive = true;
        mapSelected.transform.position = Vector3.zero;

    }

}
