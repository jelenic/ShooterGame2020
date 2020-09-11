using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : ModuleScript
{
    // Start is called before the first frame update
    private GameObject shield;

    protected override void initialize()
    {
        shield = gameObject.transform.Find("Shield").gameObject;
    }

    protected override void activeAction()
    {
        base.activeAction();
        ship.GetComponent<CombatVariables>().immune = true;
        shield.SetActive(true);
    }
    
    protected override void inactiveAction()
    {
        ship.GetComponent<CombatVariables>().immune = false;
        shield.SetActive(false);
    }
}
