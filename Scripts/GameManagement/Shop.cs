using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
    public Spawner spwn;

    public Collider2D selectedTower;

    public GameObject selectorUi;
    public TextMeshProUGUI selectorCost;
    public TextMeshProUGUI selectorLevel;

    public GameObject sellUi;
    public TextMeshProUGUI sellCost;

    private int buyPrice;

    //public bool hover;

    void Update(){

        // Place
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);

        circle.SetActive(placing);

        canPlace = !(Physics2D.OverlapCircle(mousePos, .5f, cantPlaceOn));


        if (placing == true){

            selectedTower = null;

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
                turret.GetComponent<towermanager>().spwn = spwn;
                turret.GetComponent<towermanager>().sellPrice = (int)((double)buyPrice * 0.75d);

                placing = false;
                selectedTower = null;

            }

            if (canPlace == false){

                turret.GetComponent<SpriteRenderer>().color = Color.red;

            }

        }

        // On Click

        if (placing == false) {

            if (Input.GetMouseButtonDown(0)) {

                Debug.Log("selecting");

                Collider2D[] checker = Physics2D.OverlapCircleAll(mousePos, .001f, cantPlaceOn);

                foreach (Collider2D c in checker) {

                    if (c.gameObject.GetComponent<towermanager>() != null) {

                        selectedTower = c;

                    }

                    if (turret != null) {
                        if (selectedTower == turret.GetComponent<Collider2D>().GetComponent<towermanager>()) {

                            selectedTower = null;
                            turret = null;

                        }
                    }

                }

                if (checker.Length == 0) {

                    if (!EventSystem.current.IsPointerOverGameObject()) {

                        selectedTower = null;

                    }

                }

            }

        }

        if (selectedTower != null) {

            circle.transform.position = selectedTower.transform.position;
            circle.transform.localScale = new Vector2(selectedTower.GetComponent<towermanager>().range, selectedTower.GetComponent<towermanager>().range);
            circle.SetActive(true);

            selectorUi.SetActive(true);
            sellUi.SetActive(true);

            sellCost.text = selectedTower.GetComponent<towermanager>().sellPrice.ToString();
            selectorLevel.text = "Level: " + selectedTower.GetComponent<towermanager>().level.ToString();

            selectorCost.text = ((int)Mathf.Pow(selectedTower.GetComponent<towermanager>().level * 10, 2)).ToString();

        }

        if (selectedTower == null) {

            sellUi.SetActive(false);
            selectorUi.SetActive(false);

        }

    }

    public void buyUpgrade() {

        towermanager tar = selectedTower.GetComponent<towermanager>();

        int cost = (int)Mathf.Pow(tar.level * 10, 2);

        if (cost <= gameManager.coin) {

            gameManager.coin -= cost;

            tar.damage *= 2f;
            tar.level++;

            tar.Stacks++;

            if (tar.GetComponent<endOfRoundEffect>() != null) {

                tar.GetComponent<endOfRoundEffect>().value++;

            }

        }

    }

    public void sell() {

        int cost = -(selectedTower.GetComponent<towermanager>().sellPrice);

        gameManager.coin -= cost;

        Destroy(selectedTower.gameObject);
        selectedTower = null;

    }

    public void buyTower(int towerID, int cost) {

        if (cost <= gameManager.coin && placing == false) {

            placing = true;

            gameManager.coin -= cost;
            buyPrice = cost;

            turret = Instantiate(towers[towerID], mousePos, Quaternion.identity);

        }

    }

    public void buySnail(){

        buyTower(0, 100);

    }

    public void buyBee(){

        buyTower(1, 25);

    }

   public void buyLady(){

        buyTower(4, 25);

    }

    public void buySpider(){

        buyTower(2, 50);

    }

    public void buyDragon(){

        buyTower(3, 175);

    }

    public void buyAnt(){

        buyTower(5, 150);

    }

    public void buyMantis(){

        buyTower(6, 175);
    }

    public void buyFirefly(){

        buyTower(7, 75);

    }

    public void buyShield(){

        buyTower(8, 50);

    }

    public void buyCricket(){

        buyTower(9, 150);

    }

    public void buydung(){
        buyTower(10, 200);
    }

}
