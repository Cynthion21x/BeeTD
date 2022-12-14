using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class popup : MonoBehaviour {

    public Color crit;
    public Color hit;
    public Color shield;
    public Color criket;
    public Color heal;
    public Color dot;
    public Color none;

    private TextMeshPro text;

    private float dissapearSpeed;
    private float dissapearTime;

    public bool hiding;

    private float moveVector;
    private float DamageSpeedScale;

    private int ogFontSize;
    private bool grow;

    void Awake() {

        text = this.GetComponent<TextMeshPro>();
        grow = true;

    }

    void Start() {

        hiding = false;

        StartCoroutine(DESTROY());

        dissapearTime = 1f;

        moveVector = Random.Range(-1.0f, 1.0f);

    }

    void Update() {

        //transform.position += new Vector3(moveVector, 2) * Time.deltaTime;

        if (text.fontSize <= ogFontSize * 1.5f && grow == true) {

            text.fontSize += Time.deltaTime * dissapearSpeed * 2;

        } else if (text.fontSize >= ogFontSize) {

            text.fontSize -= Time.deltaTime * dissapearSpeed * 4;
            grow = false;

        }

        if (dissapearTime < 0) {
            Destroy(this.gameObject);
        }

        if (hiding == true) {
            text.color -= new Color(0, 0, 0, Time.deltaTime * dissapearSpeed);
            dissapearTime -= Time.deltaTime * dissapearSpeed;
        }


    }

    public void Setup(float damage, string colour, int fontSize) {

        text.text = ((int)damage).ToString();

        if (colour == "crit") {
            text.color = crit;
        } else if (colour == "cricket") {
            text.color = criket;
        } else if (colour == "hit") {
            text.color = hit;
        } else if (colour == "heal") {
            text.color = heal;
        } else if (colour == "shield") {
            text.color = shield;
        } else if (colour == "dot") {
            text.color = dot;
        } else if (colour == "none") {
            text.color = none;
        }

        text.fontSize = fontSize;
        ogFontSize = (int)text.fontSize;

        transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

        dissapearSpeed = 10;

        //text.sortingOrder += (int)damage;

    }

    public IEnumerator DESTROY() {

        yield return new WaitForSeconds(1f);

        hiding = true;

    }

}
