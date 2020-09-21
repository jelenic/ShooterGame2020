using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatVariables : CombatVariables
{
    Coroutine healingCoroutine = null;
    protected override void activateDeactivateStatus(StatusEffect status, bool activate, float value)
    {
        switch (status)
        {
            case StatusEffect.Stun:
                break;
            case StatusEffect.Slowdown:
                stats.thrust = activate ? stats.og.thrust / 2 : stats.og.thrust;
                stats.rateOfFire = activate ? stats.og.rateOfFire * 2 : stats.og.rateOfFire;
                stats.projectileVelocityModifier = activate ? stats.og.projectileVelocityModifier / 2 : stats.og.projectileVelocityModifier;
                break;
            case StatusEffect.Speedup:
                stats.thrust = activate ? stats.og.thrust * 2 : stats.og.thrust;
                stats.rateOfFire = activate ? stats.og.rateOfFire / 2 : stats.og.rateOfFire;
                stats.projectileVelocityModifier = activate ? stats.og.projectileVelocityModifier * 2 : stats.og.projectileVelocityModifier;
                break;
            case StatusEffect.DamageDecrease:
                stats.damageModifier = activate ? stats.og.damageModifier / 2 : stats.og.damageModifier;
                break;
            case StatusEffect.DamageIncrease:
                stats.damageModifier = activate ? stats.og.damageModifier * 2 : stats.og.damageModifier;
                break;
            case StatusEffect.HealOverTime:
                
                if (activate)
                {
                    healingCoroutine = StartCoroutine(healOverTime());
                }
                else StopCoroutine(healingCoroutine);
                break;
        }
    }

    private IEnumerator healOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            IncreaseHP((int)(0.05f * stats.og.hp));
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


    protected override void handleStatBuff(StatBuff buff, float amount)
    {
        switch(buff)
        {
            case StatBuff.HP:
                stats.hp = (int) (stats.hp * amount);
                hp = (int) (hp * amount);
                break;
            case StatBuff.Damage:
                stats.damageModifier *= amount;
                break;
            case StatBuff.MovementSpeed:
                stats.thrust *= amount;
                break;
        }
    }
}
