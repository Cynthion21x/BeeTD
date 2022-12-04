using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloweenSkin : MonoBehaviour {

    public Sprite HalowSprite;

    void Start() {

        if (System.DateTime.Now.Month == 10) {

            this.GetComponent<SpriteRenderer>().sprite = HalowSprite;

        }

    }

}
