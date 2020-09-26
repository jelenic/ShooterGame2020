using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadFiringBasicWeapon : BasicWeaponArcade
{
    public int shotNumber;

    protected override void init()
    {
    }
    public override void fire(float playerDamagerModifier = 1, float speedModifier = 1)
    {

        for (int i=0; i < shotNumber; i++)
        {
            float current = (i + 1f) / shotNumber;
            PlayerFiredBullet pfb = Instantiate(projectile, playerTransform.position + playerTransform.up + playerTransform.right * Mathf.Lerp(-0.7f,0.7f, current),
                playerTransform.rotation * Quaternion.Euler(0f, 0f, Mathf.Lerp(15f, -15f, current))).GetComponent<PlayerFiredBullet>();
            pfb.damageModifier = playerDamagerModifier * weaponDamageModifier;
            pfb.velocityModifier = speedModifier;

        }
    }

    public override void upgrade()
    {
        rateOfFire /= 1.2f;
        weaponDamageModifier *= 1.2f;
        shotNumber = Mathf.Min(10, shotNumber + 1);
    }

}
