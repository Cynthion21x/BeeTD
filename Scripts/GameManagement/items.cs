using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Security.Cryptography;

public class items : MonoBehaviour {

    public items item1;
    public items item2;

    public itemList itemsList;

    public string type;

    public int seed;

    public TextMeshProUGUI itemText;

    public bool rare;
    public GameObject rareGlow;

    public GameObject itemList;
    public GameObject itemFrame;
    public GameObject rareItemFrame;

    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    private int id;

    void Awake() {

        type = null;

    }

    void Update() {

        rareGlow.SetActive(rare);

        if (itemsList.selecting == true) {

            if (type == null) {

                //System.Random random = new System.Random(System.DateTime.Now.Millisecond);

                //int id = random.Next(0, itemsList.item.Length);
                id = RollDice((byte)(itemsList.item.Length));

                int israte = Random.Range(1, 10);

                if (israte == 1) {

                    //id = random.Next(0, itemsList.itemR.Length);
                    id = RollDice((byte)(itemsList.itemR.Length));

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

        if (rare == false) {

            GameObject itemInList = Instantiate(itemFrame);
            itemInList.transform.parent = itemList.transform;

            itemInList.GetComponent<Image>().sprite = itemsList.itemPic[id];

        } else {

            GameObject itemInList = Instantiate(rareItemFrame);
            itemInList.transform.parent = itemList.transform;

            //itemInList.GetComponent<Image>().sprite = itemsList.itemPicR[id];
            itemInList.transform.Find("item").GetComponent<Image>().sprite = itemsList.itemPicR[id];

        }

        type = null;

        item1.type = null;
        item2.type = null;

    }


    public static byte RollDice(byte numberSides) {

        byte[] randomNumber = new byte[1];

        do {

            rngCsp.GetBytes(randomNumber);

        } while (!IsFairRoll(randomNumber[0], numberSides));

        return (byte)((randomNumber[0] % numberSides));
    }

    private static bool IsFairRoll(byte roll, byte numSides) {

        int fullSetsOfValues = System.Byte.MaxValue / numSides;

        return roll < numSides * fullSetsOfValues;
    }
}
