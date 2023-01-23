using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class towerShop : MonoBehaviour {

    [SerializeField] private Image pic;
    [SerializeField] private TextMeshProUGUI costText;

    public bool IsPowerLocked;
    public string Power;

    public int id;
    public int cost;

    private int discountedCost;
    private Button button;

    private Powers gameManager;

    public int internalIncreace;
    private int ogCost;

    void Start() {

        gameManager = GameObject.Find("GameManager").GetComponent<Powers>();
        button = this.GetComponent<Button>();

        ogCost = cost;
        
    }

    void Update() {

        if ((gameManager.power == "creation" || gameManager.power == "sandbox") && IsPowerLocked == true) {

            internalIncreace = (int)(ogCost * 0.5);

            button.interactable = true;
            pic.color = new Color(1, 1, 1);

        } else if ((gameManager.power != Power) && IsPowerLocked == true) {

            /*button.interactable = false;
            pic.color = new Color(0, 0, 0);*/

            if (gameManager.power != "no") {

                Destroy(gameObject);

            }

        }  else {

            button.interactable = true;
            pic.color = new Color(1, 1, 1);

        }

        cost = ogCost + internalIncreace;

        int totalCost = (ogCost - gameManager.shop.discount + internalIncreace);

        costText.text = totalCost.ToString();

        if (totalCost < 5) {

            costText.text = "5";

        }

        if (totalCost < ogCost) {

            costText.color = Color.green;

        } else if (totalCost > ogCost) {

            costText.color = Color.red;

        } else {

            costText.color = Color.white;

        }

    }
}
