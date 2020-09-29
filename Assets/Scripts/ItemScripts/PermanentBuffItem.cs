using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "permanent buff", menuName = "ConsumableItems/PermanentBuffItem")]

public class PermanentBuffItem : ConsumableItem
{
    public StatBuff buff;
    public float amount;

    public override bool consume(CombatVariables cv)
    {
        cv.permanentStatBuff(buff, amount);
        return true;
    }
}
