using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string name;
    public int hp;
    public int scoreValue;
    public float damageModifier;
    public float speed;
    public float range;
    public float mass;
    public float rateOfFire;
    public float turretRotationSpeed;
    public float projectileResistance;
    public float beamResistance;
    public float physicalResistance;
    public float critChance;
    public float critMultiplier;



    public float calculateFinalDmgModifier()
    {
        return (Random.value <= critChance ? critMultiplier : 1) * damageModifier;
    }
}
