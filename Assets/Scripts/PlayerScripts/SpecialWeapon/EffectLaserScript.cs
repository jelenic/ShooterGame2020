using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLaserScript : LaserScript
{
    public float duration;
    private Stats affectedStats;
    private float baseSpeed;
    protected override void specialEffect(GameObject affected)
    {
        base.specialEffect(affected);

        affectedStats = affected.GetComponent<Stats>();

        baseSpeed = affectedStats.speed;

        affectedStats.speed = 0f;

        Invoke("returnToNormal", duration);

    }


    void returnToNormal()
    {
        affectedStats.speed = baseSpeed;
    }
}
