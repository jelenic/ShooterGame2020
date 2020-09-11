using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImmunityShield : EnemyShield
{

    protected override void reactToDmg(int receivedDmg)
    {
        Debug.LogFormat("enemy received {0} dmg, hp is {1}", receivedDmg, cv.originalStats.hp);

        base.reactToDmg(receivedDmg);
        if (-10 * receivedDmg > cv.originalStats.hp)
        {
            activate();
            Debug.Log("enemy shield activated");
        }
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


    }



}
