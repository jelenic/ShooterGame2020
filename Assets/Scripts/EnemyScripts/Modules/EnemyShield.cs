using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShield : EnemyModule
{
    public bool noSprite;
    public float duration;
    public float remainingTime;
    protected CombatVariables cv;
    public float dmgThreshold;


    protected GameObject shield;
    protected SpriteRenderer shield_sprite;


    protected override void initialize()
    {
        base.initialize();
        cv = GetComponentInParent<CombatVariables>();
        cv.onHpChangedCallback += damageFilter;
        if (!noSprite)
        {
            shield = gameObject.transform.Find("Shield").gameObject;
            shield_sprite = shield.GetComponent<SpriteRenderer>();
        }
        remainingTime = duration;
    }

    protected virtual void reactToDmg(int receivedDmg) { }
    protected virtual void activateShield() { }
    protected virtual void deactivateShield()
    {
        active = false;
        StartCoroutine(cooldownCoroutine());
    }

    protected void damageFilter(int amount)
    {
        if (amount < 0)
        {
            reactToDmg(amount);
        }
    }

    private void OnDestroy()
    {
        cv.onHpChangedCallback -= damageFilter;
    }


    protected override void updateEnd()
    {
        base.updateEnd();
        if (active)
        {
            Color c = shield_sprite.color;
            c.a = remainingTime / duration + 0.1f;
            shield_sprite.color = c;
        }
    }



}
