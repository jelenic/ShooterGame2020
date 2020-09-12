using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : ModuleScript
{
    // Start is called before the first frame update
    private GameObject shield;
    private SpriteRenderer shield_renderer;

    protected override void initialize()
    {
        shield = gameObject.transform.Find("Shield").gameObject;
        shield_renderer = shield.GetComponent<SpriteRenderer>();
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

    protected override void updateAction()
    {
        base.updateAction();
        Color c = shield_renderer.color;
        c.a = remainingTime / duration + 0.1f;
        shield_renderer.color = c;
    }
}
