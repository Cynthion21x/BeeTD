using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Ui : MonoBehaviour {

    public AudioMixerGroup sound;
    public AudioMixerGroup fX;
    public Spawner spawn;

    public GameObject loadingScreen;
    public GameObject normalUI;

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

        StartCoroutine(loadingFX(name));

    }

    public void soundSet(System.Single vol){

        sound.audioMixer.SetFloat("volume", vol);

    }

     public void effectsSet(System.Single vol){

        fX.audioMixer.SetFloat("volume", vol);

    }

    public void timeSet(System.Single vol){


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
    
    private IEnumerator loadingFX(string name) {

        if (loadingScreen != null) {
            normalUI.SetActive(false);
            loadingScreen.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(1.01f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (asyncLoad.isDone != true) {
            yield return null;
        }

    }

}
