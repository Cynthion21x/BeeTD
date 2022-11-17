using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour {

    public AudioSource sound;
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

        sound.volume = vol;

    }

    public void timeSet(System.Single vol){

        Time.timeScale = vol;

    }

    public void FullscreenSet(bool value) {

        Screen.fullScreen = value;

        if (value)
            PlayerPrefs.SetInt("full", 1);
        else
            PlayerPrefs.SetInt("full", 0);

    }

    public void AutoPlaySet(bool value) { 

        if (spawn != null)
            spawn.autoplay = value;

        if (value)
            PlayerPrefs.SetInt("auto", 1);
        else
            PlayerPrefs.SetInt("auto", 0);

    }

}
