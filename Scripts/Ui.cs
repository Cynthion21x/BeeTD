using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Ui : MonoBehaviour {

    public AudioMixerGroup sound;
    public Spawner spawn;

    void Start() {

        if (!PlayerPrefs.HasKey("full")) {

            PlayerPrefs.SetInt("full", 1);
            Screen.fullScreen = true;

        }

    }

    public void exit() {

        Application.Quit();

    }

    public void load(string name){

        SceneManager.LoadScene(name);

    }

    public void soundSet(System.Single vol){

        sound.audioMixer.SetFloat("volume", vol);

    }

    public void timeSet(System.Single vol){

        Time.timeScale = vol;

        if(SceneManager.GetActiveScene().name == "MainMenu") {

            Time.timeScale = 1f;

        }

    }

    public void FullscreenSet(bool value) {

        Screen.fullScreen = value;

    }

    public void AutoPlaySet(bool value) { 

        if (spawn != null) {
            spawn.autoplay = value;
        }

    }

}
