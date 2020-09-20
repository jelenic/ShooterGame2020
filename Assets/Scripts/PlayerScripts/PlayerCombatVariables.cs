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

    protected override void ifHPzero()
    {
        StartCoroutine(playerDeath());
    }

    private IEnumerator playerDeath()
    {
        Debug.Log("player deathh");
        yield return new WaitForSeconds(0.5f);
        if (hp == 0) LevelManager.instance.die();
        Destroy(gameObject);
    }
}
