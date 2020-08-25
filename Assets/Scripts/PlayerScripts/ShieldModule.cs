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
        ship.GetComponent<CombatVariables>().involunrable = true;
        shield.SetActive(true);
    }
    
    protected override void inactiveAction()
    {
        ship.GetComponent<CombatVariables>().involunrable = false;
        shield.SetActive(false);
    }
}
