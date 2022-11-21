using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {

    public AudioSource microwaveBeep;

    void OnMouseDown() {

        microwaveBeep.Play();

    }

}
