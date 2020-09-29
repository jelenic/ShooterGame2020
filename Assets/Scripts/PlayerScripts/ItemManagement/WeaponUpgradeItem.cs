using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgradeItem", menuName = "ConsumableItems/WeaponUpgradeItem")]

public class WeaponUpgradeItem : ConsumableItem
{
    public override bool consume(CombatVariables cv)
    {
        cv.weaponUpgrade();
        return true;
    }
}
