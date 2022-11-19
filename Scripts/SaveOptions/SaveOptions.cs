using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveOptions : MonoBehaviour {

    public AudioSource sound;
    public Slider soundSlider;

    public Slider timeSlider;

    public Toggle fulscreen;
    public Toggle AutoPlay;

    public Spawner spawn;

    void Start(){

        loadValuesFloat();

    }

    public void loadValuesFloat(){

        soundSlider.value = PlayerPrefs.GetFloat("vol");
        sound.volume = PlayerPrefs.GetFloat("vol");

        timeSlider.value = PlayerPrefs.GetFloat("time");
        Time.timeScale = PlayerPrefs.GetFloat("time");

        if (Time.timeScale <= 0) {
            Time.timeScale = 1;
        }

        if (PlayerPrefs.GetInt("full") == 1){

            Screen.fullScreen = true;
            fulscreen.isOn = true;

        } else {

            Screen.fullScreen = false;
            fulscreen.isOn = false;

        }

        if (PlayerPrefs.GetInt("auto") == 1){

            if (spawn != null) {
                spawn.autoplay = true;
            }

            AutoPlay.isOn = true;

        } else {

            if (spawn != null) {
                spawn.autoplay = false;
            }

            AutoPlay.isOn = false;

        }

    }

    public void saveFloat(){

        PlayerPrefs.SetFloat("vol", soundSlider.value);
        PlayerPrefs.SetFloat("time", timeSlider.value);

        if (AutoPlay.isOn) {

            PlayerPrefs.SetInt("auto", 1);

        } else {

            PlayerPrefs.SetInt("auto", 0);

        }

        if (fulscreen.isOn) {

            PlayerPrefs.SetInt("full", 1);

        } else {

            PlayerPrefs.SetInt("full", 0);

        }

    }

}
