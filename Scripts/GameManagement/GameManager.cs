using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    
    public int hp = 10;
    public int coin = 100;

    public float windSpeed = 5;
    public Spawner spawner;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI coinText;
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
        int num = random.Next(0, 5);

        string[] easy = { "Awh not even past easy mode", "Its called easy for a reason", "you didnt use the meta", "Rip", "what were you thinking", "use a diffrent power next time"  };
        string[] norm = { "A fair place to die", "Nothing special happens here", "you died", "Boss a bit annoying huh", "flying enemies get you every time", "these should be more personalised" };
        string[] tricky = { "Thats so crazy", "It is quite tricky to be fair", "You ran the meta", "siuvgmcsdoibgcmvbfdgoibcmvfoismdbcvo fgo", "going for the new record?", "better luck next time" };

        easystring = easy[num];
        normalstring = norm[num];
        tricktstring = tricky[num]; 

    }

    void Update(){

        windSpeed = Mathf.Round(Mathf.Abs(Mathf.Sin((float)spawner.Wave * 2)) * 700f) / 100f;

        hpText.text = (hp).ToString();
        coinText.text = (coin).ToString();

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

}
