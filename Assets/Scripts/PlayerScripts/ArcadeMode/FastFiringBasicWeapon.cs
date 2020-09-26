﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFiringBasicWeapon : BasicWeaponArcade
{
    public override void fire(float playerDamagerModifier = 1, float speedModifier = 1)
    {
        PlayerFiredBullet pfb = Instantiate(projectile, playerTransform.position, playerTransform.rotation).GetComponent<PlayerFiredBullet>();
        pfb.damageModifier = playerDamagerModifier * weaponDamageModifier;
        pfb.velocityModifier = speedModifier;
    }

    public override void upgrade()
    {
        rateOfFire /= 1.2f;
        weaponDamageModifier *= 1.2f;
    }
}
