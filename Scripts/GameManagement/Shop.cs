using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour {

    public GameObject upgradeEffect;

    public bool mouseOverUi;
    public GameObject placeingUI;
    public GameObject placeHiddenUi;

    public GameManager gameManager;

    public Vector2 mousePos;

    public GameObject[] towers;

    public Camera main;

    public LayerMask cantPlaceOn;
    public LayerMask noGround;

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
    public GameObject activUi;
    public TextMeshProUGUI activCost;

    private int buyPrice;

    public int damageBonus = 0;
    public float damageBonusMultiple = 1f;
    public float weightReduce = 0f;

    public float baseDamageBoost;

    public int discount;
    public float abilitydiscount;

    int UILayer;

    //public bool hover;

    void Start() {

        UILayer = LayerMask.NameToLayer("UI");

    }

    void Update(){

        towermanager bug = null;
        SpriteRenderer bugSprite = null;

        if (turret != null) {

            bug = turret.GetComponent<towermanager>();
            bugSprite = turret.GetComponent<SpriteRenderer>();

        }

        // Place
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);

        circle.SetActive(placing);

        canPlace = !(Physics2D.OverlapCircle(mousePos, .5f, cantPlaceOn));

        if (turret != null && bug.flying == false) {

            if (Physics2D.OverlapCircle(mousePos, .2f, noGround)) {

                canPlace = false;

            }

        }

        if (placing == true){

            selectedTower = null;

            bugSprite.color = Color.grey;
            bug.enabled = false;
            turret.GetComponent<BoxCollider2D>().enabled = false;
            turret.transform.position = mousePos;

            circle.transform.position = mousePos;
            circle.transform.localScale = new Vector2(turret.GetComponent<towermanager>().range, turret.GetComponent<towermanager>().range);

            circle.GetComponent<SpriteRenderer>().color = new Color32(47, 114, 255, 101);

            if (Input.GetMouseButtonUp(0) && canPlace && IsPointerOverUIElement() == false) {

                bugSprite.color = Color.white;
                bug.enabled = true;
                turret.GetComponent<BoxCollider2D>().enabled = true;

                bug.Set();
                bug.spwn = spwn;
                bug.sellPrice = (int)((double)buyPrice * 0.75d);

                placing = false;
                selectedTower = null;

                placeHiddenUi.SetActive(true);

            }

            if (canPlace == false){

                bugSprite.color = Color.red;
                circle.GetComponent<SpriteRenderer>().color = new Color32(255, 25, 63, 101);

            }

        }

        // On Click

        if (placing == false) {

            if (Input.GetMouseButtonDown(0) && IsPointerOverUIElement() == false) {

                //Debug.Log("selecting");

                Collider2D[] checker = Physics2D.OverlapCircleAll(mousePos, .001f, cantPlaceOn);

                foreach (Collider2D c in checker) {

                    if (c.gameObject.GetComponent<towermanager>() != null) {

                        selectedTower = c;

                    }

                    if (turret != null && IsPointerOverUIElement() == false) {

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

            if (selectedTower.GetComponent<activate>() != null) {

                activUi.SetActive(true);
                activCost.text = selectedTower.GetComponent<activate>().Cost.ToString();

            } else {

                activUi.SetActive(false);

            }

            sellCost.text = selectedTower.GetComponent<towermanager>().sellPrice.ToString();
            selectorLevel.text = "Level: " + selectedTower.GetComponent<towermanager>().level.ToString();

            selectorCost.text = ((int)Mathf.Pow(selectedTower.GetComponent<towermanager>().level * 10, 2)).ToString();

        }

        if (selectedTower == null) {

            sellUi.SetActive(false);
            selectorUi.SetActive(false);
            activUi.SetActive(false);

        }

        placeingUI.SetActive(placing);

        if (placing) {

            placeHiddenUi.SetActive(false);

        }

    }

    public bool IsPointerOverUIElement() {

        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
        return EventSystem.current.IsPointerOverGameObject();

    }

    public void ButtonUpgrade() {

        buyUpgrade();

    }

    public void buyUpgrade(towermanager tar = null, bool nocost = false) {

        if (tar == null) {

            tar = selectedTower.GetComponent<towermanager>();

        }

        int cost = (int)Mathf.Pow(tar.level * 10, 2);

        if (nocost) { cost = 0; }

        if (cost <= gameManager.coin) {

            gameManager.coin -= cost;

            tar.level++;

            tar.sellPrice += (int)(cost * 0.75f);

            Instantiate(upgradeEffect, tar.transform.position, Quaternion.identity);

            if (tar.GetComponent<endOfRoundEffect>() != null) {

                tar.GetComponent<endOfRoundEffect>().value++;

            }

            if (tar.gameObject.name == "centipede(Clone)") {

                tar.baseDamage += 0.5f;

            }

            if (tar.gameObject.name == "shield(Clone)") {

                tar.fireRate -= 0.5f;

            }

        }

    }

    public void FreeUpgrade(towermanager tar) {

        buyUpgrade(tar, true);

    }

    public void buyAbility() {

        int cost = selectedTower.GetComponent<activate>().Cost;

        if (cost <= gameManager.energy) {

            gameManager.energy -= cost;
            selectedTower.GetComponent<activate>().trigger();

        }

    }

    public void sell() {

        int cost = -(selectedTower.GetComponent<towermanager>().sellPrice);

        gameManager.coin -= cost;

        Destroy(selectedTower.gameObject);
        selectedTower = null;

    }

    public void cancell() {

        if (placing == true) {
            gameManager.coin += buyPrice;

            Destroy(turret.gameObject);
            placing = false;
        }

        placing = false;
        placeHiddenUi.SetActive(true);

    }


    public void buyTower(int towerID, int cost) {

        int dcost = cost - discount;

        if (dcost <= 5) {

            dcost = 5;

        }

        if (dcost <= gameManager.coin && placing == false) {

            placing = true;

            gameManager.coin -= dcost;
            buyPrice = dcost;

            turret = Instantiate(towers[towerID], mousePos, Quaternion.identity);

        }

    }

    public void buyFromShop(towerShop insect) {

        buyTower(insect.id, insect.cost);

    }

}
