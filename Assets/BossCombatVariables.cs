using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatVariables : EnemyCombatVariables
{

    protected override void initialize()
    {
        hp = 10000000;
        arcadeManager = ArcadeManager.instance;

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

}
