using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achievements : MonoBehaviour {

    public achievementLoader achievementLoader;

    private Sprite Image;
    public Sprite Locked;

    private Image background;
    public Image LockedImage;

    public Color selectedColor;
    public Color DefaultColor;

    public int id;

    [SerializeField]private bool isSelected;
    [SerializeField]private bool isUnlocked;

    void Start() {

        if (!PlayerPrefs.HasKey("sA")) {

            PlayerPrefs.SetInt("sA", -1);

        }

        Image = LockedImage.sprite;

        background = GetComponent<Image>();

        isUnlocked = false;

        int[] Unlockedachievements = achievementLoader.achievements;

        foreach (int i in Unlockedachievements) {

            if ( i == id ) {

                isUnlocked = true;

            }

        }


    }

    void Update() {

        if (PlayerPrefs.GetInt("sA") == id) {

            isSelected = true;

        } else {

            isSelected = false;

        }
        

        if (isSelected == true) {

            background.color = selectedColor;

        } else {

            background.color = DefaultColor;

        }

        if (!isUnlocked) {

            LockedImage.sprite = Locked;
             
        } else {

            LockedImage.sprite = Image;

        }

    }

    public void Select() {

        if (isUnlocked) {

            PlayerPrefs.SetInt("sA", id);

        }

        if (isSelected == true) {

            PlayerPrefs.SetInt("sA", -1);

        }

    }

}
