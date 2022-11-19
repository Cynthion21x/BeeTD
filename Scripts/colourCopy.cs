using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colourCopy : MonoBehaviour {

    public SpriteRenderer original;

    void Update() {

        this.gameObject.GetComponent<SpriteRenderer>().color = original.color;

    }
}
