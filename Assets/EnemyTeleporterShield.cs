using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleporterShield : EnemyShield
{
    protected override void reactToDmg(int receivedDmg)
    {
        base.reactToDmg(receivedDmg);
        if (-receivedDmg / dmgThreshold > cv.stats.og.hp)
        {
            activate();
            Debug.Log("enemy teleport activated");
        }


    }
    protected override void activateModule()
    {
        Debug.Log("before teleport " + parentTransform.position);
        Vector3 randomPos = 5f * Random.insideUnitCircle;

        parentTransform.Translate(randomPos + Vector3.up*10f, Space.Self);
        Debug.Log("after teleport " + parentTransform.position);

        deactivateShield();
    }

    protected override void updateEnd() { }


    protected override void deactivateShield()
    {
        base.deactivateShield();
    }
}
