﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatVariables : CombatVariables
{
    public GameObject hp_bar_object;
    public float hideHP;



    protected override void activateDeactivateStatus(StatusEffect status, bool activate, float amount = 1f) // amounts need to be changed in places where statuses are applied
    {
        switch (status)
        {
            case StatusEffect.Stun:
                stats.speed = activate ? 0 : stats.og.speed;
                stats.angleSpeed = activate ? 0 : stats.og.angleSpeed;
                stats.rateOfFire = activate ? 5f : stats.og.rateOfFire; // special way to stun is needed, perhaps as a state on enemyAI
                stats.turretRotationSpeed = activate ? 0f : stats.og.turretRotationSpeed;
                break;
            case StatusEffect.NoMovement:
                stats.speed = activate ? 0f : stats.og.speed;
                stats.angleSpeed = activate ? 0f : stats.og.angleSpeed;
                stats.turretRotationSpeed = activate ? 0f : stats.og.turretRotationSpeed;
                break;
            case StatusEffect.Slowdown:
                float ratio = 2f;
                float ratio2 = 5f;
                stats.speed = activate ? stats.og.speed / ratio : stats.og.speed;
                stats.angleSpeed = activate ? stats.og.angleSpeed / ratio : stats.og.angleSpeed;
                stats.rateOfFire = activate ? stats.og.rateOfFire * ratio2 : stats.og.rateOfFire;
                stats.turretRotationSpeed = activate ? stats.og.turretRotationSpeed / ratio : stats.og.turretRotationSpeed;
                stats.projectileVelocityModifier = activate ? stats.og.projectileVelocityModifier / ratio2 : stats.og.projectileVelocityModifier;
                break;
            case StatusEffect.MovementSpeedup:
                stats.speed = activate ? stats.og.speed * 2 : stats.og.speed;
                stats.angleSpeed = activate ? stats.og.angleSpeed * 2 : stats.og.angleSpeed;
                stats.turretRotationSpeed = activate ? stats.og.turretRotationSpeed * 2 : stats.og.turretRotationSpeed;
                break;
            case StatusEffect.ProjectileSpeedup:
                stats.projectileVelocityModifier = activate ? stats.og.projectileVelocityModifier * 2 : stats.og.projectileVelocityModifier;
                break;
            case StatusEffect.RateOfFireUp:
                stats.rateOfFire = activate ? stats.og.rateOfFire / 2 : stats.og.rateOfFire;
                break;
            case StatusEffect.DamageDecrease:
                stats.damageModifier = activate ? stats.og.damageModifier / 2 : stats.og.damageModifier;
                break;
            case StatusEffect.DamageIncrease:
                stats.damageModifier = activate ? stats.og.damageModifier * 2 : stats.og.damageModifier;
                break;
        }
    }

    protected override void initialize()
    {
        hideHP = PlayerPrefs.GetFloat("hp_timer", 3f);
        if (hideHP.Equals(0f)) hp_bar_object.SetActive(false);
        else if (hideHP.Equals(15f)) hp_bar_object.SetActive(true);
        else
        {
            hp_bar_object.SetActive(false);
            StartCoroutine(hideHpBar());
        }
    }

    protected override void changeHpBar(float filled)
    {
        if (!PlayerPrefs.GetFloat("hp_timer", 3f).Equals(0f)) hp_bar_object.SetActive(true);
        base.changeHpBar(filled);

        hideHP = PlayerPrefs.GetFloat("hp_timer", 3f);
    }

    IEnumerator hideHpBar()
    {
        while (true)
        {
            hideHP = Mathf.Max(0f, hideHP - 0.5f);
            if (hideHP.Equals(0f)) hp_bar_object.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    protected override void onDestroy()
    {
        arcadeManager.enemyDeath();
        if (hp.Equals(0)) levelManager.increaseScore(stats.scoreValue, stats.name);
    }
}
