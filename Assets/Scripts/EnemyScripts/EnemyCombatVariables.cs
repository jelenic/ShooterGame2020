using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatVariables : CombatVariables
{
    protected override void activateDeactivateStatus(StatusEffect status, bool activate, float value)
    {
        switch (status)
        {
            case StatusEffect.Stun:
                stats.speed = activate ? 0 : originalStats.speed;
                stats.angleSpeed = activate ? 0 : originalStats.angleSpeed;
                stats.rateOfFire = activate ? value : originalStats.rateOfFire;
                break;
            case StatusEffect.Slowdown:
                stats.speed = activate ? originalStats.speed / 2 : originalStats.speed;
                stats.angleSpeed = activate ? originalStats.angleSpeed / 2 : originalStats.angleSpeed;
                stats.rateOfFire = activate ? originalStats.rateOfFire * 2 : originalStats.rateOfFire;
                break;
            case StatusEffect.Speedup:
                stats.speed = activate ? originalStats.speed * 2 : originalStats.speed;
                stats.angleSpeed = activate ? originalStats.angleSpeed * 2 : originalStats.angleSpeed;
                stats.rateOfFire = activate ? originalStats.rateOfFire / 2 : originalStats.rateOfFire;
                break;
            case StatusEffect.DamageDecrease:
                stats.damageModifier = activate ? originalStats.damageModifier / 2 : originalStats.damageModifier;
                break;
            case StatusEffect.DamageIncrease:
                stats.damageModifier = activate ? originalStats.damageModifier * 2 : originalStats.damageModifier;
                break;
        }
    }
}
