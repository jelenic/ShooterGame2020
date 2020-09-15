using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;



public class CombatVariables : MonoBehaviour, Damageable
{
    protected Stats stats;

    public OriginalStats originalStats;

    public int hp;

    public Image hpBar;

    private Dictionary<DamageType, float> resistances;
    private Dictionary<DamageType, Color> dmgColor;

    public GameObject floatingNumberText;

    Transform transform;

    private LevelManager levelManager;

    public bool immune;

    public List<StatusEffect> currentlyAfflicted = new List<StatusEffect>();

    public delegate void onHpChanged(int amount);
    public onHpChanged onHpChangedCallback;

    public void createFloatingNumberText(Vector2 position, Color color, string text = "oops")
    {
        if (color == null) color = Color.white;
        GameObject floatingNumber = Instantiate(floatingNumberText, position, Quaternion.identity);
        TextMesh tm = floatingNumber.GetComponent<TextMesh>();
        tm.text = text;
        tm.color = color;
    }

    


    public int DecreaseHP(int amount, DamageType dmgType = DamageType.Default)
    {
        if (immune)
        {
            Debug.Log("involunrable");
            return hp;
        }
        else
        {
            int receivedDmg = Math.Max(1, (int)Math.Round(amount * (1f - resistances[dmgType])));
            //Debug.LogFormat("{0} received {1} dmg of type {2}, original amount: {3}", stats.name, receivedDmg, dmgType, amount);
            hp = Math.Max(0, hp - receivedDmg);
            createFloatingNumberText(transform.position, dmgColor[dmgType], receivedDmg.ToString());
            if (hp == 0)
            {
                Destroy(gameObject);
                return 0;
            }
            hpBar.enabled = true;
            hpBar.fillAmount = (float)hp / stats.hp;

            if (onHpChangedCallback != null) onHpChangedCallback.Invoke(receivedDmg * (-1));
            return hp;
        }
    }

    public int IncreaseHP(int amount)
    {
        hp = Math.Min(stats.hp, hp + amount);
        //Debug.LogFormat("object {0} hp increased by {1}, current hp: {2}", gameObject.tag, amount, hp);
        hpBar.fillAmount = (float)hp / stats.hp;
        if (onHpChangedCallback != null) onHpChangedCallback.Invoke(amount);

        return hp;
    }
    void Start()
    {
        floatingNumberText = Resources.Load("FloatingNumberText") as GameObject;
        transform = GetComponent<Transform>();
        stats = GetComponent<Stats>();

        originalStats = new OriginalStats(stats.speed, stats.angleSpeed, stats.rateOfFire, stats.turretRotationSpeed, stats.damageModifier, stats.projectileVelocityModifier, stats.hp);

        hp = stats.hp;
        //Debug.LogFormat("total hp: {0}", stats.hp);
        dmgColor = new Dictionary<DamageType, Color>();
        dmgColor.Add(DamageType.Projectile, Color.red);
        dmgColor.Add(DamageType.Beam, Color.blue);
        dmgColor.Add(DamageType.Physical, Color.gray);
        dmgColor.Add(DamageType.Default, Color.white);


        resistances = new Dictionary<DamageType, float>();
        resistances.Add(DamageType.Projectile, stats.projectileResistance);
        resistances.Add(DamageType.Beam, stats.beamResistance);
        resistances.Add(DamageType.Projectile, stats.physicalResistance);
        resistances.Add(DamageType.Default, 0f);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        immune = false;
    }


    IEnumerator stopStatus(StatusEffect status, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentlyAfflicted.Remove(status);
        activateDeactivateStatus(status, false, 0f);
    }

    private void activateDeactivateStatus(StatusEffect status, bool activate, float value)
    {
        switch(status){
            case StatusEffect.Stun:
                stats.speed = activate ? 0 : originalStats.speed;
                stats.angleSpeed = activate ? 0 : originalStats.angleSpeed;
                stats.rateOfFire = activate ? value : originalStats.rateOfFire;
                break;
            case StatusEffect.Slowdown:
                stats.speed = activate ? originalStats.speed / 2 : originalStats.speed;
                stats.angleSpeed = activate ? originalStats.angleSpeed / 2 : originalStats.angleSpeed;
                stats.rateOfFire = activate ? originalStats.rateOfFire * 2 : originalStats.rateOfFire;
                break;
            case StatusEffect.Speedup:
                stats.speed = activate ? originalStats.speed * 2 : originalStats.speed;
                stats.angleSpeed = activate ? originalStats.angleSpeed * 2 : originalStats.angleSpeed;
                stats.rateOfFire = activate ? originalStats.rateOfFire / 2 : originalStats.rateOfFire;
                break;
            case StatusEffect.DamageDecrease:
                stats.damageModifier = activate ? originalStats.damageModifier / 2 : originalStats.damageModifier;
                break;
            case StatusEffect.DamageIncrease:
                stats.damageModifier = activate ? originalStats.damageModifier * 2 : originalStats.damageModifier;
                break;
        }
    }
    public void inflictStatus(StatusEffect status, float duration = 20f)
    {
        Debug.Log(gameObject.name + " getting inflicted by " + status.ToString());
        Debug.Log(originalStats.speed);
        Debug.Log(stats.speed);
        if (!currentlyAfflicted.Contains(status))
        {
            currentlyAfflicted.Add(status);
            StartCoroutine(stopStatus(status, duration));
            activateDeactivateStatus(status, true, duration);
        }
    }

    private void OnDestroy()
    {
        levelManager.increaseScore(stats.scoreValue);
    }

    public struct OriginalStats
    {
        public float speed; public float angleSpeed; public float rateOfFire; public float turretRotationSpeed; public float damageModifier; public float projectileVelocityModifier; public int hp;
        public OriginalStats(float speed, float angleSpeed, float rateOfFire, float turretRotationSpeed, float damageModifier, float projectileVelocityModifier, int hp)
        {
            this.speed = speed; this.angleSpeed = angleSpeed; this.rateOfFire = rateOfFire; this.turretRotationSpeed = turretRotationSpeed; this.damageModifier = damageModifier; this.projectileVelocityModifier = projectileVelocityModifier; this.hp = hp;
        }
    }
}


public enum StatusEffect
{
    Stun,
    Slowdown,
    Speedup,
    DamageDecrease,
    DamageIncrease,
}

public enum DamageType
{
    Beam,
    Physical,
    Projectile,
    Default
}