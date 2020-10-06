using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombatVariables : CombatVariables
{
    private EquipementScript es;
    public TextMeshProUGUI currentHPText;
    public TextMeshProUGUI maxHPText;
    private ShipFireArcade sfa;
    private bool playerDied;

    protected override void initialize()
    {
        es = GetComponent<EquipementScript>();
        sfa = GetComponent<ShipFireArcade>();
        StartCoroutine(refreshHP());

    }

    public override void weaponUpgrade()
    {
        sfa.weaponUpgrade();
    }

    IEnumerator refreshHP()
    {
        yield return new WaitForSeconds(0.2f);
        changeHpBar(hp / stats.hp);
    }

    protected override void changeHpBar(float filled)
    {
        if (currentHPText != null)
        { 
            currentHPText.text = hp.ToString();
            maxHPText.text = stats.hp.ToString();
        }
        base.changeHpBar(filled);
    }

    Coroutine healingCoroutine = null;
    protected override void activateDeactivateStatus(StatusEffect status, bool activate, float amount)
    {
        switch (status)
        {
            case StatusEffect.Stun:
                break;
            case StatusEffect.Slowdown:
                stats.thrust = activate ? stats.og.thrust / amount : stats.og.thrust;
                stats.rateOfFire = activate ? stats.og.rateOfFire * amount : stats.og.rateOfFire;
                stats.projectileVelocityModifier = activate ? stats.og.projectileVelocityModifier / amount : stats.og.projectileVelocityModifier;
                break;
            case StatusEffect.MovementSpeedup:
                stats.thrust = activate ? stats.og.thrust * amount : stats.og.thrust;
                break;
            case StatusEffect.RateOfFireUp:
                stats.rateOfFire = activate ? stats.og.rateOfFire / amount : stats.og.rateOfFire;
                break;
            case StatusEffect.EnergyUp:
                stats.energyRechargeSpeed = activate ? stats.og.energyRechargeSpeed * amount : stats.og.energyRechargeSpeed;
                break;
            case StatusEffect.ProjectileSpeedup:
                stats.projectileVelocityModifier = activate ? stats.og.projectileVelocityModifier * amount : stats.og.projectileVelocityModifier;
                break;
            case StatusEffect.DamageDecrease:
                stats.damageModifier = activate ? stats.og.damageModifier / amount : stats.og.damageModifier;
                break;
            case StatusEffect.DamageIncrease:
                stats.damageModifier = activate ? stats.og.damageModifier * amount : stats.og.damageModifier;
                break;
            case StatusEffect.DefenseUp:
                stats.beamResistance = activate ? stats.og.beamResistance * amount : stats.og.beamResistance;
                stats.physicalResistance = activate ? stats.og.physicalResistance * amount : stats.og.physicalResistance;
                stats.projectileResistance = activate ? stats.og.projectileResistance * amount : stats.og.projectileResistance;
                break;
            case StatusEffect.HealOverTime:
                
                if (activate)
                {
                    healingCoroutine = StartCoroutine(healOverTime(amount));
                }
                else StopCoroutine(healingCoroutine);
                break;
        }
    }

    private IEnumerator healOverTime(float amount = 0.07f)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            IncreaseHP((int)(amount * stats.og.hp));
        }
    }

    protected override void ifHPzero()
    {
        if (!playerDied) StartCoroutine(playerDeath());
    }

    private IEnumerator playerDeath()
    {
        playerDied = true;
        //Debug.Log("player deathh");
        yield return new WaitForSeconds(0.5f);
        changeHpBar(0f);
        LevelManager.instance.die();
        Destroy(gameObject);
    }


    protected override void handleStatBuff(StatBuff buff, float amount)
    {
        switch(buff)
        {
            case StatBuff.HP:
                stats.hp = (int) (stats.hp * amount);
                hp = (int) (hp * amount);
                changeHpBar((float) hp / stats.hp);
                break;
            case StatBuff.Damage:
                stats.damageModifier *= amount;
                break;
            case StatBuff.MovementSpeed:
                stats.thrust *= amount;
                break;
            case StatBuff.MagazineSize:
                stats.magazineModifier += (int) amount;
                break;
            case StatBuff.Energy:
                stats.energyRechargeSpeed *= amount;
                stats.totalEnergy *= amount;
                break;
            case StatBuff.ProjectileSpeed:
                stats.projectileVelocityModifier *= amount;
                break;
            case StatBuff.RateOfFire:
                stats.rateOfFire /= amount;
                break;
            case StatBuff.Defense:
                stats.beamResistance *= amount;
                stats.projectileResistance *= amount;
                stats.physicalResistance *= amount;
                break;

        }
    }

    public override bool handleEquipement(Equipement eq)
    {
        return es.Equip(eq);
    }


}
