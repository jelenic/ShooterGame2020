using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImmunityShield : EnemyShield
{

    protected override void reactToDmg(int receivedDmg)
    {
        Debug.LogFormat("enemy received {0} dmg, hp is {1}", receivedDmg, cv.stats.og.hp);

        base.reactToDmg(receivedDmg);
        if (-receivedDmg / dmgThreshold > cv.stats.og.hp)
        {
            activate();
            Debug.Log("enemy shield activated");
        }
    }

    protected override void activateModule()
    {
        base.activateModule();
        remainingTime = duration;
        activateShield();
    }

    protected override void activateShield()
    {
        base.activateShield();
        shield.SetActive(true);
        cv.immune = true;
        Debug.Log("enemy immune");
    }

    protected override void deactivateShield()
    {
        base.deactivateShield();
        shield.SetActive(false);
        cv.immune = false;
        Debug.Log("enemy mune");
        base.deactivateShield();
    }

    protected override void updateStart()
    {
        base.updateStart();
        if (active)
        {
            remainingTime = Mathf.Max(0, remainingTime - Time.deltaTime);
            if (remainingTime.Equals(0f))
            {
                deactivateShield();
            }
        }
    }



}
