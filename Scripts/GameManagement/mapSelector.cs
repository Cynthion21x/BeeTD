using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapSelector : MonoBehaviour {

    public map[] maps;

    void Start() {

        int mapSelected = Random.Range(0, maps.Length);

        maps[mapSelected].isActive = true;
        maps[mapSelected].gameObject.SetActive(true);

    }

}
