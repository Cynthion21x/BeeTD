using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour {

    public AudioSource sound;
    public Spawner spawn;

    void Start(){

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

    public void AutoPlaySet(bool value) { 

        spawn.autoplay = value;

    }

}
