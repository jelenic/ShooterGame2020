using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretModuleLaser : RotateTowardsTarget
{
    protected override void Initialize()
    {
        stats = gameObject.GetComponentInParent<Stats>();
    }

    protected override void fire()
    {
    }
}
