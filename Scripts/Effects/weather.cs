using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather : MonoBehaviour {

    public GameObject[] weathers;
    public mapSelector map;

    private bool DoWeather;
    [SerializeField] private int weatherType;

    void Start() {

        weatherType = 0;
        map = GameObject.Find("Maps").GetComponent<mapSelector>();

        InvokeRepeating("Rotate", 0, 40);

    }

    void Update() {

        DoWeather = map.weatherOn;

        if (DoWeather) {

            for (int i = 0; i < weathers.Length; i++) {

                if (i == weatherType) {

                    weathers[i].SetActive(true);

                } else {

                    weathers[i].SetActive(false);

                }

            }

        } else {

            for (int i = 0; i < weathers.Length; i++) {

                weathers[i].SetActive(false);

            }

        }

    }

    private void Rotate() {

        Debug.Log("Picking Weather Event");

        int decider = Random.Range(0, 100);

        if (decider > 70) {

            Debug.Log("Switching Weather");

            weatherType = Random.Range(0, weathers.Length);

        }

    }
}
