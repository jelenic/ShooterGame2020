using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : SpecialWeaponScript
{
    public GameObject waveBounds;
    public GameObject waveBoundsInstance;
    public ShockwaveController shockwaveController;

    public float activeTime;
    public float activeFor;
    public float reachedChargeLevel;


    public void effectDamage(GameObject affected)
    {
        Damageable d = affected.GetComponent<Damageable>();
        d.DecreaseHP((int)(stats.calculateFinalDmgModifier() * dmgBase * reachedChargeLevel), dmgType);
        CombatVariables cv = affected.GetComponent<CombatVariables>();
        if (cv != null) cv.inflictStatus(StatusEffect.Slowdown);
    }

    protected override void initialize()
    {
        activeTime = 0.64f;
    }

    protected override void onChargeBegin()
    {
        waveBoundsInstance = Instantiate(waveBounds, transform);
        shockwaveController = waveBoundsInstance.GetComponent<ShockwaveController>();
    }

    protected override void onChargeChange()
    {
        shockwaveController.chargeRefresh(calculateCharge() / maxCharge);
    }


    protected override void stuff(float modifier = 1)
    {

        shockwaveController.setParams(effectDamage);
        activeFor = activeTime * (calculateCharge() / maxCharge);

        Debug.LogWarning("charge end " + reachedChargeLevel);
        reachedChargeLevel = calculateCharge();
        Debug.LogWarning("charge enddd " + reachedChargeLevel);
    }

    protected override void updateStart()
    {
        activeFor -= Time.deltaTime;
        if (shockwaveController != null && shockwaveController.effectActive) shockwaveController.wave(1.1f);

    }

    protected override void updateFinish()
    {
        if (activeFor <= 0f)
        {
            if (waveBoundsInstance != null && shockwaveController.effectActive)
            {
                Destroy(waveBoundsInstance);
                shockwaveController = null;
            }
        }
    }
}
