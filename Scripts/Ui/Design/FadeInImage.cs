using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum FadeAction {

    FadeIn,
    FadeOut,
}

public class FadeInImage : MonoBehaviour {

    [SerializeField] private FadeAction fadeType;
    [SerializeField] private Image img;

    public float max = 1f;

    void Awake() {

        if (img == null) {

            img = this.GetComponent<Image>();

        }

    }


    void OnEnable() {

        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);

        if (fadeType == FadeAction.FadeIn) {

            StartCoroutine(FadeIn());

        }

        else if (fadeType == FadeAction.FadeOut) {

            StartCoroutine(FadeOut());

        }

    }

    IEnumerator FadeIn() {

        for (float i = 0; i <= max; i += Time.unscaledDeltaTime * 2) {

            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

    }


    IEnumerator FadeOut() {

        for (float i = 1; i >= 0; i -= Time.deltaTime) {

            img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            yield return null;
        }

    }

}