using UnityEngine;

public abstract class ConsumableItem : Item
{
    public Sprite pickUpIcon = null;
    public Color pickUpIconColor = Color.white;
    public float disappearTime;

    public virtual bool consume(CombatVariables cv) { return false; }
}

