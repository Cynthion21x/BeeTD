using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : MonoBehaviour {

    void OnDestroy() {

        if (this.GetComponent<EnemyController>().hp <= 0) {
            GameObject.Find("GameManager").GetComponent<itemList>().selecting = true;
        }

    }

}
