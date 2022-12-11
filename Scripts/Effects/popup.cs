using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class popup : MonoBehaviour {

    private TextMeshPro text;

    private float dissapearSpeed;
    private float dissapearTime;

    public bool hiding;

    private float moveVector;
    private float DamageSpeedScale;

    private int ogFontSize;

    void Awake() {

        text = this.GetComponent<TextMeshPro>();

    }

    void Start() {

        hiding = false;

        StartCoroutine(DESTROY());

        dissapearTime = 1f;

        moveVector = Random.Range(-1.0f, 1.0f);

    }

    void Update() {

        //transform.position += new Vector3(moveVector, 2) * Time.deltaTime;

        if (text.fontSize < ogFontSize * 1.25f) {

            text.fontSize += Time.deltaTime * dissapearSpeed;

        } else if (text.fontSize > ogFontSize) {

            text.fontSize -= Time.deltaTime * dissapearSpeed * 2;

        }

        if (dissapearTime < 0) {
            Destroy(this.gameObject);
        }

        if (hiding == true) {
            text.color -= new Color(0, 0, 0, Time.deltaTime * dissapearSpeed);
            dissapearTime -= Time.deltaTime * dissapearSpeed;
        }


    }

    public void Setup(float damage, Color colour, int fontSize) {

        text.text = damage.ToString();
        text.color = colour;
        text.fontSize = fontSize;
        ogFontSize = (int)text.fontSize;

        transform.position += new Vector3(Random.Range(-1.3f, 1.3f), Random.Range(-1.3f, 1.3f), 0);

        dissapearSpeed = 10;

    }

    public IEnumerator DESTROY() {

        yield return new WaitForSeconds(.5f);

        hiding = true;

    }

}
