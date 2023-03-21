using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemList : MonoBehaviour {

    public string[] item;
    public Sprite[] itemPic;
    public string[] itemDesc;
    public string[] itemQuote;

    public string[] itemR;
    public Sprite[] itemPicR;
    public string[] itemDescR;
    public string[] itemQuoteR;

    public bool selecting;

    public GameObject itemMenu;
    public GameManager hp;

    private float time;
    private bool timer;

    void Start() {
        time = Time.timeScale; 
    }

    void Update() {

        itemMenu.SetActive(selecting);

        if (selecting) {
            
            if (Time.timeScale != 0) { time = Time.timeScale; }

            //Time.timeScale = 0f;
            timer = false;
        
        } else {
            
            if (timer == false)
                Time.timeScale = time;
                timer = true;
           
        }
        
        if (hp.hp <= 0) {

            itemMenu.SetActive(false);

        }

    }

}
