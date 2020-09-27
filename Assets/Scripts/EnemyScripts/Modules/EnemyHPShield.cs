using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPShield : EnemyShield
{
    protected int dmgStack;
    protected override void reactToDmg(int receivedDmg)
    {

        base.reactToDmg(receivedDmg);
        if (!active) dmgStack -= receivedDmg; // since its negative
        if (!active && dmgStack >= dmgThreshold*cv.stats.og.hp)
        {
            activate();
            dmgStack = 0;
            //Debug.Log("enemy hp shield activated");
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
        //Debug.Log("enemy hp shield active");
    }

    protected override void deactivateShield()
    {
        shield.SetActive(false);
        //Debug.Log("enemy hp shield ded");
        base.deactivateShield();

    }

    protected override void activateModule()
    {
        base.activateModule();
        remainingTime = duration*cv.stats.og.hp;
        activateShield();
    }

    protected override void updateEnd()
    {
        base.updateEnd();
        if (active)
        {
            Color c = shield_sprite.color;
            c.a = remainingTime / (duration * cv.stats.og.hp);
            shield_sprite.color = c;
        }
    }



}
