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

    void Update() {

        itemMenu.SetActive(selecting);

        
        if (hp.hp <= 0) {

            itemMenu.SetActive(false);

        }

    }

}
