using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBasicWeapon : BasicWeaponArcade
{
    public override void fire(float playerDamagerModifier = 1, float speedModifier = 1)
    {
        PlayerFiredMine pfm = Instantiate(projectile, playerTransform.position + playerTransform.up, playerTransform.rotation).GetComponent<PlayerFiredMine>();
        pfm.damageModifier = playerDamagerModifier * weaponDamageModifier;
    }

    public override void upgrade()
    {
        rateOfFire /= 1.1f;
        weaponDamageModifier *= 1.3f;
    }
}
