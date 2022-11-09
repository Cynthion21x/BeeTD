using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameManager gameManager;

    public Vector2 mousePos;

    public GameObject[] towers;

    public Camera main;

    public LayerMask cantPlaceOn;

    public bool placing = false;
    public bool canPlace = true;
    public GameObject turret;

    public GameObject circle;

    void Update(){

        mousePos = main.ScreenToWorldPoint(Input.mousePosition);

        circle.SetActive(placing);

        canPlace = !(Physics2D.OverlapCircle(mousePos, .5f, cantPlaceOn));

        /*
        if (Physics2D.OverlapCircle(mousePos, .5f, cantPlaceOn)){

            if (turret.GetComponent<towermanager>().flying == true && Physics2D.OverlapCircle(mousePos, .5f, cantPlaceOn).GetComponent<towermanager>().flying == false) {

                canPlace = true;

            } else{

                canPlace = false;

            }

        } else {

            canPlace = true;

        }
        */

        if (placing == true){

            turret.GetComponent<SpriteRenderer>().color = Color.grey;
            turret.GetComponent<towermanager>().enabled = false;
            turret.GetComponent<BoxCollider2D>().enabled = false;
            turret.transform.position = mousePos;

            circle.transform.position = mousePos;
            circle.transform.localScale = new Vector2(turret.GetComponent<towermanager>().range, turret.GetComponent<towermanager>().range);

            if (Input.GetMouseButtonDown(0) && canPlace){

                turret.GetComponent<SpriteRenderer>().color = Color.white;
                turret.GetComponent<towermanager>().enabled = true;
                turret.GetComponent<BoxCollider2D>().enabled = true;

                turret.GetComponent<towermanager>().Set();

                placing = false;

            }

            if (canPlace == false){

                turret.GetComponent<SpriteRenderer>().color = Color.red;

            }

        }

    }

    public void buySnail(){

        int cost = 100;

        if (cost <= gameManager.coin && placing == false) {

            placing = true;

            gameManager.coin -= cost;
            turret = Instantiate(towers[0], mousePos, Quaternion.identity);

        }

    }

    public void buyBee(){

        int cost = 50;

        if (cost <= gameManager.coin && placing == false) {

            placing = true;

            gameManager.coin -= cost;
            turret = Instantiate(towers[1], mousePos, Quaternion.identity);

        }

    }

    public void buySpider(){

        int cost = 25;

        if (cost <= gameManager.coin && placing == false) {

            placing = true;

            gameManager.coin -= cost;
            turret = Instantiate(towers[2], mousePos, Quaternion.identity);

        }

    }

        public void buyDragon(){

        int cost = 175;

        if (cost <= gameManager.coin && placing == false) {

            placing = true;

            gameManager.coin -= cost;
            turret = Instantiate(towers[3], mousePos, Quaternion.identity);

        }

    }

}
