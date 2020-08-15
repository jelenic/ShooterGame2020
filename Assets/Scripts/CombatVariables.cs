using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatVariables : MonoBehaviour
{
    private Stats stats;

    public int hp;

    public Image hpBar;

    private Dictionary<string, float> resistances;


    public int DecreaseHP(int amount, string dmgType = "default")
    {
        int receivedDmg = (int)Math.Round(amount * (1f - resistances[dmgType]));
        Debug.LogFormat("{0} received {1} dmg of type {2}, original amount: {3}", stats.name, receivedDmg, dmgType, amount);
        hp = Math.Max(0, hp - receivedDmg);
        //Debug.LogFormat("object {0} hp decreased by {1}, current hp: {2}", gameObject.tag, amount, hp);
        if (hp == 0) Destroy(gameObject);
        hpBar.enabled = true;
        hpBar.fillAmount = (float)hp / stats.hp;
        return hp;
    }

    public int InreaseHP(int amount)
    {
        hp = Math.Min(stats.hp, hp + amount);
        //Debug.LogFormat("object {0} hp increased by {1}, current hp: {2}", gameObject.tag, amount, hp);
        hpBar.fillAmount = (float)hp / stats.hp;
        return hp;
    }
    void Start()
    {
        stats = GetComponent<Stats>();
        hp = stats.hp;
        Debug.LogFormat("total hp: {0}", stats.hp);
        resistances = new Dictionary<string, float>();
        resistances.Add("projectile", stats.projectileResistance);
        resistances.Add("beam", stats.beamResistance);
        resistances.Add("physical", stats.physicalResistance);
        resistances.Add("default", 0f);
    }


}
