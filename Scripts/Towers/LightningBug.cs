using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBug : MonoBehaviour {

    public towermanager self;
    public SpriteRenderer eyes;

    public GameManager gameManager;

    public GameObject[] effects;
    public float[] ranges;
    public float[] damagebonuses;

    public Sprite flyingSprite;
    public Sprite defaultSprite;

    void Start() {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Update() {

        float fullness = (float)gameManager.energy / (float)gameManager.Ecap;

        self.baseDamage = 1 + fullness * 9;

        eyes.color = new Color(fullness, fullness, fullness);

        if (fullness >= 1) {

            self.meleeEffect = effects[2];
            self.baseDamage = 12;
            self.baseRange = ranges[2];

            if (self.flying == false) {

                self.flying = true;

                self.GetComponent<SpriteRenderer>().sprite = flyingSprite;

            }

        } else if (fullness >= 0.5) {

            self.meleeEffect = effects[1];
            self.baseRange = ranges[1];

            if (self.flying == true) {

                self.flying = false;

                self.GetComponent<SpriteRenderer>().sprite = defaultSprite;

                Destroy(self.flyingChild);

            }

        } else {

            self.meleeEffect = effects[0];
            self.range = ranges[0];

            if (self.flying == true) {

                self.flying = false;

                self.GetComponent<SpriteRenderer>().sprite = defaultSprite;

                Destroy(self.flyingChild);

            }

        }

    }

}
