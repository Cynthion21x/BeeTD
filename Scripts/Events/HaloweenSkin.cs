using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasSkin : MonoBehaviour {

    public Sprite ChristmasSprite;

    void Start() {

        if (System.DateTime.Now.Month == 12) {

            this.GetComponent<SpriteRenderer>().sprite = ChristmasSprite;

        }

    }

}
