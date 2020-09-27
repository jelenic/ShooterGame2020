using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleporterShield : EnemyShield
{
    protected override void reactToDmg(int receivedDmg)
    {
        base.reactToDmg(receivedDmg);
        if (-receivedDmg / 0.02f > cv.stats.og.hp)
        {
            activate();
            Debug.Log("enemy teleport activated");
        }


    }
    protected override void activateModule()
    {
        Debug.Log("before teleport " + parentTransform.position);
        Vector3 randomPos = 10f * Random.insideUnitCircle;

        parentTransform.Translate(randomPos, Space.Self);
        Debug.Log("after teleport " + parentTransform.position);

        deactivateShield();
    }

    protected override void updateEnd() { }


    protected override void deactivateShield()
    {
        cooldownActive = true;
        active = false;
    }
}
