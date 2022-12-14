using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class rainbowUI : MonoBehaviour {

    Color lerpedColor = Color.blue;

    void Update() {

        lerpedColor = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.unscaledTime, 1));

        this.GetComponent<Image>().color = lerpedColor;

        this.GetComponent<RectTransform>().Rotate(Vector3.forward * Time.unscaledDeltaTime * 45);

    }

}

