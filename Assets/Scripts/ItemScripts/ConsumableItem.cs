using UnityEngine;

public abstract class ConsumableItem : Item
{
    public Sprite pickUpIcon = null;
    public Color pickUpIconColor = Color.white;

    public virtual void consume(CombatVariables cv) { }
}
