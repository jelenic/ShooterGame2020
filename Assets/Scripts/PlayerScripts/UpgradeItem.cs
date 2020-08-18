using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : Item
{
    public override void Initialize()
    {
        base.Initialize();
        type = "UpgradeItem";
    }
}
