using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPlaceEffect : MonoBehaviour {

    private towermanager tower;
    [SerializeField] private string effectName;

    void Start() {

        tower = GetComponent<towermanager>();
        
    }

    public void Activate(GameManager gameManager) {

        StartCoroutine(activateDelay(gameManager));
        
    }

    public IEnumerator activateDelay(GameManager gameManager) {

        yield return new WaitForEndOfFrame();

        
        switch (effectName) {

            case "Titanbug":

                tower.damageBoost += gameManager.coin / 4;
                tower.sellPrice += gameManager.coin;
                gameManager.coin = 0;

                break;

        }

    }

}
