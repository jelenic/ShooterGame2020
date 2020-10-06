using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New buff item", menuName = "ConsumableItems/BuffItem")]

public class BuffItem : ConsumableItem
{
    public StatusEffect effect;
    public float duration;
    public float amount;

    public override bool consume(CombatVariables cv)
    {
        if (effect.Equals(StatusEffect.Heal)) cv.IncreaseHP((int)(amount*cv.stats.hp));
        else cv.inflictStatus(effect, amount, duration);
        return true;
    }
}
