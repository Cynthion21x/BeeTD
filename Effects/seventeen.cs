using UnityEngine;
public class seventeen : MonoBehaviour {

    public GameManager game;
    public GameObject effect;

    void Update() {

        effect.SetActive(

            game.hp == 17 && game.coin == 17 && game.energy == 17

            );

    }

}