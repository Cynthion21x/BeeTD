using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingTower : MonoBehaviour {

    public GameObject perentTower;

    public BoxCollider2D collide = null;

    void Update() {

        GameObject manager = GameObject.Find("GameManager");

        if(manager.GetComponent<Shop>().placing) {

            this.GetComponent<SpriteRenderer>().sprite  = perentTower.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().color = new Color(256, 0, 0, 1);

        } else {

            this.GetComponent<SpriteRenderer>().color = new Color(256, 0, 0, 0);

        }

        if (collide == null) {

            collide = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;

        }

        collide.size = perentTower.GetComponent<BoxCollider2D>().size;

        this.gameObject.layer = perentTower.layer;

        this.transform.localScale = perentTower.transform.localScale;

        this.transform.rotation = perentTower.transform.rotation;

        collide.isTrigger = true;

    }
}
