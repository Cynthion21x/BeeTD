using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    
    public int hp = 10;
    public int coin = 100;
    public int energy = 0;

    public int EgainRate;
    public int Ecap;

    public Color fullColor;

    public float windSpeed = 5;
    public Spawner spawner;
    public bool altwind;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI windText;

    public TextMeshProUGUI endMessage;
    public TextMeshProUGUI endCoin;
    public TextMeshProUGUI endWave;

    public GameObject died;

    private string easystring;
    private string normalstring;
    private string tricktstring;

    void Start(){

        System.Random random = new System.Random();

        string[] easy = { "Awh not even past easy mode", "Its called easy for a reason", "you didnt use the meta", "sorry to say but it dosent get any easier than this", "what were you thinking", "use a diffrent power next time", "Unlucky", "skill t-issue", "the wasps are mean", "i think its best to just not say anything", "thats a little bit cringe", "perhaps there is a greater meaning behind all these messages", "I wonder where the most people die"  };
        string[] norm = { "where better to die than normal mode", "Nothing special happens here", "you died", "Boss a bit annoying huh", "flying enemies get you every time", "these should be more personalised", "better luck next time", "you got bee'd", "these messages are decided beffore you even play", "theres not much more i can say other than 'you died'", "i just write these when im bored", "don't blame the randomness", "seems to be windy everywhere these days" };
        string[] tricky = { "Thats so crazy", "It is quite tricky to be fair", "You ran the meta", "siuvgmcsdoibgcmvbfdgoibcmvfoismdbcvofgo", "going for the new record?", "better luck next time", "wowzers", "im supprised you didn't quit at the first wave", "this message is the best reward your gonna get", "surely these messages get boring", "go out side and see some real insects", "you did this", "I don't call it tricky mode for nothing"  };

        int num = random.Next(0, easy.Length);

        easystring = easy[num];
        normalstring = norm[num];
        tricktstring = tricky[num];

        InvokeRepeating("GainEnergy", 1f, 1f);

    }

    void Update(){

        windSpeed = Mathf.Round(Mathf.Abs(Mathf.Sin((float)spawner.Wave * 2)) * 700f) / 100f;

        if (altwind) {

            windSpeed = Mathf.Round(Mathf.Sin((float)spawner.Wave) * 1400f) / 100f;

        }

        hpText.text = (hp).ToString();
        coinText.text = (coin).ToString();
        energyText.text = (energy).ToString();

        if (energy >= Ecap) {

            energyText.color = fullColor;

        } else {

            energyText.color = new Color(1, 1, 1, 1);

        }

        windText.text = (windSpeed).ToString() + " mph";

        if (hp <= 0){

            spawner.autoplay = false;

            died.SetActive(true);

            endCoin.text = (coin).ToString();
            endWave.text = (spawner.Wave).ToString();

            if (spawner.mode == "easy") {

                endMessage.text = "\"" + easystring + "\"";

            } else if (spawner.mode == "regular") {

                endMessage.text = "\"" + normalstring + "\"";

            } else {

                endMessage.text = "\"" + tricktstring + "\"";

            }

        }

    }

    public void GainEnergy() {

        if (GameObject.FindGameObjectWithTag("Enemy")) {

            if (energy < Ecap) {

                energy += EgainRate;

            }

        }

    }

}
