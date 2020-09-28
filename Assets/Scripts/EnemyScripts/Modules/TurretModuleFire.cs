using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretModuleFire : RotateTowardsTarget
{
    protected override void Initialize()
    {
        stats = gameObject.GetComponentInParent<Stats>();
    }
}


