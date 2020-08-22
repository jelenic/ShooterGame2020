using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredHoming : FiredProjectile
{
    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 3;
        projectileDamageType = "projectile";

        lifeDuration = 10f;

        passThrough.Add("Enemy");
        passThrough.Add("Spawner");
        passThrough.Add("Projectile");

        damageable.Add("Player");
    }




}

