using UnityEngine;

public class DropShadow : MonoBehaviour {

    public Vector2 offset;
    public int OrderInLayer;

    public Color colour;

    private SpriteRenderer caster;
    private SpriteRenderer shadow;

    private Transform shadowTransform;

    void Start() {

        shadowTransform = new GameObject().transform;
        shadowTransform.parent = transform;

        shadowTransform.name = "shady shadow";

        shadowTransform.localRotation = Quaternion.identity;
        shadowTransform.localScale = Vector3.one;

        caster = GetComponent<SpriteRenderer>();
        shadow = shadowTransform.gameObject.AddComponent<SpriteRenderer>();

        shadow.sortingLayerName = caster.sortingLayerName;
        shadow.sortingOrder = OrderInLayer;

        shadow.color = colour;

    }

    void LateUpdate() {

        shadowTransform.position = (Vector2)transform.position + offset;

        shadow.sprite = caster.sprite;

    }

}