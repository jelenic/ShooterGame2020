using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public new string name;

    public float mass;

    //defenses related
    public int hp;
    public float projectileResistance;
    public float beamResistance;
    public float physicalResistance;

    //damage related
    public float damageModifier;
    public float critChance;
    public float critMultiplier;
    public float projectileVelocityModifier;

    //enemy only
    public int scoreValue;
    public float range;
    public float angleSpeed;
    public float rateOfFire;
    public float turretRotationSpeed;
    public float speed;

    //player only
    public float maxVelocity;
    public float thrust;




    public float calculateFinalDmgModifier()
    {
        return (Random.value <= critChance ? critMultiplier : 1) * damageModifier;
    }
}



public struct OriginalStats // for making duplicates which is annoyingly hard with monobehaviour classes
{
    public int hp;
    public float mass;
    public float projectileResistance;
    public float beamResistance;
    public float physicalResistance;
    public float damageModifier;
    public float critChance;
    public float critMultiplier;
    public float projectileVelocityModifier;
    public int scoreValue;
    public float range;
    public float angleSpeed;
    public float rateOfFire;
    public float turretRotationSpeed;
    public float speed;
    public float maxVelocity;
    public float thrust;
    public OriginalStats(Stats stats)
    {
        this.hp = stats.hp;
        this.mass = stats.mass;
        this.projectileResistance = stats.projectileResistance;
        this.beamResistance = stats.beamResistance;
        this.physicalResistance = stats.physicalResistance;
        this.damageModifier = stats.damageModifier;
        this.critChance = stats.critChance;
        this.critMultiplier = stats.critMultiplier;
        this.projectileVelocityModifier = stats.projectileVelocityModifier;
        this.scoreValue = stats.scoreValue;
        this.range = stats.range;
        this.angleSpeed = stats.angleSpeed;
        this.rateOfFire = stats.rateOfFire;
        this.turretRotationSpeed = stats.turretRotationSpeed;
        this.speed = stats.speed;
        this.maxVelocity = stats.maxVelocity;
        this.thrust = stats.thrust;
}
}
