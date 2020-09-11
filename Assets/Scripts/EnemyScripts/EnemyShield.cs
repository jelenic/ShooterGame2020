using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShield : EnemyModule
{
    public float duration;
    public float remainingTime;
    protected CombatVariables cv;

    protected GameObject shield;



    protected override void initialize()
    {
        base.initialize();
        cv = GetComponentInParent<CombatVariables>();
        cv.onHpChangedCallback += damageFilter;
        shield = gameObject.transform.Find("Shield").gameObject;
        remainingTime = duration;
    }

    protected virtual void reactToDmg(int receivedDmg) { }
    protected virtual void activateShield() { }
    protected virtual void deactivateShield() { }

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

    protected override void activateModule()
    {
        base.activateModule();
        remainingCooldown += duration;
        remainingTime = duration;
        //Debug.Log("shield activated remaining time = " + remainingTime);
        activateShield();
    }




    protected override void updateStart()
    {
        base.updateStart();
        if (active)
        {
            remainingTime = Mathf.Max(0, remainingTime - Time.deltaTime);
            //Debug.Log("remaining time decreased to " + remainingTime);
        }
        if (active && remainingTime.Equals(0f))
        { 
            deactivateShield();
        }
    }



}
