using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatVariables : EnemyCombatVariables
{

    protected override void initialize()
    {
        arcadeManager.setBossDetails(stats.name);
    }

    protected override void ifHPzero()
    {
        arcadeManager.bossDeath();
        Destroy(gameObject);
    }

    protected override void changeHpBar(float filled)
    {
        arcadeManager.updateBossHP(hp, stats.hp);
    }

    protected override void onDestroy()
    {
        arcadeManager.bossDeath();
    }

    protected override void handleStatBuff(StatBuff buff, float amount)
    {
        switch (buff)
        {
            case StatBuff.HP:
                stats.hp = (int)(stats.hp * amount);
                hp = (int)(hp * amount);
                changeHpBar((float)hp / stats.hp);
                break;
            case StatBuff.Damage:
                stats.damageModifier *= amount;
                break;
            case StatBuff.MovementSpeed:
                stats.thrust *= amount;
                break;
            case StatBuff.RateOfFire:
                stats.rateOfFire /= amount;
                break;
        }
    }

}
