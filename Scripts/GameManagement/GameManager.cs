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

    void Update(){

        windSpeed = Mathf.Round(Mathf.Sin((float)spawner.Wave * 1.5f) + 7 * 100f) / 100f;

        hpText.text = (hp).ToString();
        coinText.text = (coin).ToString();

        windText.text = (windSpeed).ToString() + " mph";

        if (hp <= 0){

            spawner.autoplay = false;

            died.SetActive(true);

            endCoin.text = (coin).ToString();
            endWave.text = (spawner.Wave).ToString();

            if (spawner.mode == "easy") {

                endMessage.text = "Its called easy mode for a reason";

            } else if (spawner.mode == "regular") {

                endMessage.text = "Just wait until tricky mode";

            } else {

                endMessage.text = "Do not worry many dont get here";

            }

        }

    }

}
