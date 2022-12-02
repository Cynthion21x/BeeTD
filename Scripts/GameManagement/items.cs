using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class items : MonoBehaviour {

    public itemList itemsList;

    private string type;

    public int seed;

    public TextMeshProUGUI itemText;

    public bool rare;
    public GameObject rareGlow;

    void Start() {

        seed += System.DateTime.Now.Millisecond;

    }

    void Update() {

        rareGlow.SetActive(rare);

        if (itemsList.selecting == true) {

            if (type == null) {

                int id = Random.Range(0, itemsList.item.Length);

                Random.InitState(seed + System.DateTime.Now.Millisecond);

                int israte = Random.Range(1, 5);

                if (israte == 1) {

                    this.GetComponent<Image>().sprite = itemsList.itemPicR[id];
                    type = itemsList.itemR[id];

                    this.GetComponent<Button>().onClick.RemoveAllListeners();

                    this.GetComponent<Button>().onClick.AddListener(SelectItem);

                    itemText.text = "< " + type + "> \n" + itemsList.itemDescR[id] + "\n" + itemsList.itemQuoteR[id];

                    rare = true;

                } else {

                    this.GetComponent<Image>().sprite = itemsList.itemPic[id];
                    type = itemsList.item[id];

                    this.GetComponent<Button>().onClick.RemoveAllListeners();

                    this.GetComponent<Button>().onClick.AddListener(SelectItem);

                    itemText.text = "< " + type + "> \n" + itemsList.itemDesc[id] + "\n" + itemsList.itemQuote[id];

                    rare = false;

                }

            }
            
        } else {

            type = null;

        }
       
    }

    void SelectItem() {

        itemsList.selecting = false;
        this.GetComponent<Image>().sprite = null;

        GameObject.Find("GameManager").GetComponent<Powers>().items.Add(type);

        type = null;

    }

}
