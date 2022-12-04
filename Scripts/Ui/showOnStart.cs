using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showOnStart : MonoBehaviour {
    
    public GameObject objectToShow;

    void Start() {

        objectToShow.SetActive(true);

    }
        

}
