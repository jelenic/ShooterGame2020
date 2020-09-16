using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatVariables : CombatVariables
{
    protected override void activateDeactivateStatus(StatusEffect status, bool activate, float value)
    {
        switch (status)
        {
            case StatusEffect.Stun:
                
                break;
            case StatusEffect.Slowdown:
                
                break;
            case StatusEffect.Speedup:
                break;
            case StatusEffect.DamageDecrease:
                break;
            case StatusEffect.DamageIncrease:
                break;
        }
    }
}
