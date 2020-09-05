using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLaserScript : LaserScript
{
    
    protected override void specialEffect(GameObject affected)
    {
        base.specialEffect(affected);

        affected.GetComponent<CombatVariables>().inflictStatus((StatusEffect)Random.Range(0, System.Enum.GetValues(typeof(StatusEffect)).Length));
        //affected.GetComponent<CombatVariables>().inflictStatus(StatusEffect.Speedup);

    }

}
