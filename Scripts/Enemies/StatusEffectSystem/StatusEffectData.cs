using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffect")]
public class StatusEffectData : ScriptableObject {

    public string Name;
    public GameObject effect;

    public bool percentDamage;

    public float dotAmmount;
    public float movement;
    public int coinBoost;
    public float armourShred;

    public float tickSpeed;
    public float lifeTime;

}
