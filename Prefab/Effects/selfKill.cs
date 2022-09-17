using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfKill : MonoBehaviour{

    public float time;

    void Start(){

        StartCoroutine(kill(time));

    }

    private IEnumerator kill(float Time) {

        yield return new WaitForSeconds(Time);
        Destroy(gameObject);

    }

}
