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
    public OriginalStats
        (
        int hp,
        float mass,
        float projectileResistance,
        float beamResistance,
        float physicalResistance,
        float damageModifier,
        float critChance,
        float critMultiplier,
        float projectileVelocityModifier,
        int scoreValue,
        float range,
        float angleSpeed,
        float rateOfFire,
        float turretRotationSpeed,
        float speed,
        float maxVelocity,
        float thrust
        )
    {
        this.hp = hp;
        this.mass = mass;
        this.projectileResistance = projectileResistance;
        this.beamResistance = beamResistance;
        this.physicalResistance = physicalResistance;
        this.damageModifier = damageModifier;
        this.critChance = critChance;
        this.critMultiplier = critMultiplier;
        this.projectileVelocityModifier = projectileVelocityModifier;
        this.scoreValue = scoreValue;
        this.range = range;
        this.angleSpeed = angleSpeed;
        this.rateOfFire = rateOfFire;
        this.turretRotationSpeed = turretRotationSpeed;
        this.speed = speed;
        this.maxVelocity = maxVelocity;
        this.thrust = thrust;
}
}
