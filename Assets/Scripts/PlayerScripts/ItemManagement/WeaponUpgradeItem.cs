using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgradeItem", menuName = "ConsumableItems/WeaponUpgradeItem")]

public class WeaponUpgradeItem : ConsumableItem
{
    public override void consume(CombatVariables cv)
    {
        cv.weaponUpgrade();
    }
}
