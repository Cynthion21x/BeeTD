using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveOptions : MonoBehaviour {

    public AudioSource sound;
    public Slider soundSlider;

    void Start(){

        loadSound();

    }

    public void loadSound(){

        soundSlider.value = PlayerPrefs.GetFloat("vol");
        sound.volume = PlayerPrefs.GetFloat("vol");

    }

    public void saveSound(){

        PlayerPrefs.SetFloat("vol", soundSlider.value);

    }

}
