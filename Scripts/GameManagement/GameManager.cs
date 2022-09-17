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

    void Update(){

        windSpeed = Mathf.Round(Mathf.Cos((float)spawner.Wave) + 7 * 1.15078f * 100f) / 100f;

        hpText.text = (hp).ToString();
        coinText.text = (coin).ToString();

        windText.text = (windSpeed).ToString() + " mph";

        if (hp <= 0){

            //endgame and such

        }

    }

}
