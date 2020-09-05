using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;



public class CombatVariables : MonoBehaviour
{
    private Stats stats;

    OriginalStats originalStats;

    public int hp;

    public Image hpBar;

    private Dictionary<string, float> resistances;

    public GameObject floatingNumberText;

    Transform transform;

    private LevelManager levelManager;

    public bool involunrable;

    public List<StatusEffect> currentlyAfflicted = new List<StatusEffect>();

    public void createFloatingNumberText(Vector2 position, Color color, string text = "oops")
    {
        if (color == null) color = Color.white;
        GameObject floatingNumber = Instantiate(floatingNumberText, position, Quaternion.identity);
        TextMesh tm = floatingNumber.GetComponent<TextMesh>();
        tm.text = text;
        tm.color = color;
    }

    


    public int DecreaseHP(int amount, string dmgType = "default")
    {
        if (involunrable)
        {
            Debug.Log("involunrable");
            return hp;
        }
        else
        {
            int receivedDmg = Math.Max(1, (int)Math.Round(amount * (1f - resistances[dmgType])));
            //Debug.LogFormat("{0} received {1} dmg of type {2}, original amount: {3}", stats.name, receivedDmg, dmgType, amount);
            hp = Math.Max(0, hp - receivedDmg);
            createFloatingNumberText(transform.position, Color.red, receivedDmg.ToString());
            if (hp == 0) Destroy(gameObject);
            hpBar.enabled = true;
            hpBar.fillAmount = (float)hp / stats.hp;
            return hp;
        }
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
        floatingNumberText = Resources.Load("FloatingNumberText") as GameObject;
        transform = GetComponent<Transform>();
        stats = GetComponent<Stats>();

        originalStats = new OriginalStats(stats.speed, stats.angleSpeed, stats.rateOfFire, stats.turretRotationSpeed, stats.damageModifier, stats.projectileVelocityModifier);

        hp = stats.hp;
        //Debug.LogFormat("total hp: {0}", stats.hp);
        resistances = new Dictionary<string, float>();
        resistances.Add("projectile", stats.projectileResistance);
        resistances.Add("beam", stats.beamResistance);
        resistances.Add("physical", stats.physicalResistance);
        resistances.Add("default", 0f);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        involunrable = false;
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
        public float speed; public float angleSpeed; public float rateOfFire; public float turretRotationSpeed; public float damageModifier; public float projectileVelocityModifier;
        public OriginalStats(float speed, float angleSpeed, float rateOfFire, float turretRotationSpeed, float damageModifier, float projectileVelocityModifier)
        {
            this.speed = speed; this.angleSpeed = angleSpeed; this.rateOfFire = rateOfFire; this.turretRotationSpeed = turretRotationSpeed; this.damageModifier = damageModifier; this.projectileVelocityModifier = projectileVelocityModifier;
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