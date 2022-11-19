using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private Vector2 Pos;
    private Vector2 startPos;

    public int moveModifier;

    void Start() {

        startPos = transform.position;

    }

    void Update() {

        Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, startPos.x + (Pos.x * moveModifier) / 40, 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, startPos.y + (Pos.y * moveModifier) / 40, 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);

    }

}
