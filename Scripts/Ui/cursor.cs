using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour {

    public enum ViewType {

        screen,
        world

    }

    public ParticleSystem clickEffect;

    public ViewType view;


    void Start() {

        Cursor.visible = false;

    }

    void Update() {

        Vector2 mousePosition = Input.mousePosition;
        
        if (view == ViewType.world) {

            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition); 

        } else {

            transform.position = (Vector2)mousePosition;

        }

        
        if (Input.GetMouseButtonDown(0)) {

            clickEffect.Play();

        }

    }
}
