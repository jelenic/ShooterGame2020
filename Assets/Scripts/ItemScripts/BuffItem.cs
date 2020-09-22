using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New buff item", menuName = "ConsumableItems/BuffItem")]

public class BuffItem : ConsumableItem
{
    public StatusEffect effect;
    public float duration;
    public int amount;

    public override void consume(CombatVariables cv)
    {
        if (effect.Equals(StatusEffect.Heal)) cv.IncreaseHP(amount);
        else cv.inflictStatus(effect, duration);
    }
}
