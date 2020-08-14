using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredHoming : FiredProjectile
{
    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 3;
        lifeDuration = 10f;
        Debug.LogFormat("homing bullet modifier: {0}", damageModifier);
    }




}

