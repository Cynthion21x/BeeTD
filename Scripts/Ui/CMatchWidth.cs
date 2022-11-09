using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class CMatchWidth : MonoBehaviour {

    public float sceneWidth = 10;

    Camera camera;

    void Start() {

        camera = GetComponent<Camera>();

    }

    void Update() {

        //Debug.Log(camera.aspect);

        if (camera.aspect > 1.6) {

            float unitsPerPixel = sceneWidth / Screen.width;

            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

            camera.orthographicSize = desiredHalfHeight;

        } else {

            camera.orthographicSize = 5;

        }
        
    }
}