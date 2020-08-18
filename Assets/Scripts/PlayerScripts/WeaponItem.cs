using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public int dmg;

    public override void Initialize()
    {
        base.Initialize();
        type = "WeaponItem";
    }
}
