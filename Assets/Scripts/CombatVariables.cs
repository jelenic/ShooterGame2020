﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatVariables : MonoBehaviour
{
    private Stats stats;

    public int hp;

    public Image hpBar;


    public int DecreaseHP(int amount)
    {
        hp = Math.Max(0, hp - amount);
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
    }


    void Update()
    {
        
    }
}
