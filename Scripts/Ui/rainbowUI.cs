using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class rainbowUI : MonoBehaviour {

    Color lerpedColor = Color.blue;

    void Update() {

        lerpedColor = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.time, 1));

        this.GetComponent<Image>().color = lerpedColor;

    }

}

