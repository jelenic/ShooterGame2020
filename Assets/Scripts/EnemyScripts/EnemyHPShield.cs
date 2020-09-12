using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPShield : EnemyShield
{
    public int dmgStack = 0;
    public int dmgThreshold;
    protected override void reactToDmg(int receivedDmg)
    {

        base.reactToDmg(receivedDmg);
        if (!active) dmgStack -= receivedDmg; // since its negative
        if (!active && dmgStack >= dmgThreshold)
        {
            activate();
            dmgStack = 0;
            Debug.Log("enemy hp shield activated");
        }

    }

    public void getDmg(int amount)
    {
        remainingTime += amount;
        if (remainingTime <= 0f)
        {
            deactivateShield();
        }
    }


    protected override void activateShield()
    {
        base.activateShield();
        shield.SetActive(true);
        Debug.Log("enemy hp shield active");
    }

    protected override void deactivateShield()
    {
        base.deactivateShield();
        shield.SetActive(false);
        Debug.Log("enemy hp shield ded");
        cooldownActive = true;
        active = false;

    }

    protected override void activateModule()
    {
        base.activateModule();
        remainingTime = duration;
        activateShield();
    }



}
