using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInSprite : MonoBehaviour {

    [SerializeField] private SpriteRenderer img;

    public float max = 1f;

    void Awake() {

        if (img == null) {

            img = this.GetComponent<SpriteRenderer>();

        }

    }


    void Start() {

        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);

        StartCoroutine(FadeIn());

    }

    IEnumerator FadeIn() {

        for (float i = 0; i <= max; i += Time.unscaledDeltaTime * 2) {

            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        for (float i = 1; i >= 0; i -= Time.unscaledDeltaTime * 2) {

            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;

        }

    }

}