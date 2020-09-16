using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string name;

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
    public float speed; public float angleSpeed; public float rateOfFire; public float turretRotationSpeed; public float damageModifier; public float projectileVelocityModifier; public int hp;
    public OriginalStats(float speed, float angleSpeed, float rateOfFire, float turretRotationSpeed, float damageModifier, float projectileVelocityModifier, int hp)
    {
        this.speed = speed; this.angleSpeed = angleSpeed; this.rateOfFire = rateOfFire; this.turretRotationSpeed = turretRotationSpeed; this.damageModifier = damageModifier; this.projectileVelocityModifier = projectileVelocityModifier; this.hp = hp;
    }
}
