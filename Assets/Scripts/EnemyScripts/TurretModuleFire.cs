using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretModuleFire : RotateTowardsTarget
{
    protected override void SetStats()
    {
        stats = gameObject.GetComponentInParent<Stats>();
    }
}
