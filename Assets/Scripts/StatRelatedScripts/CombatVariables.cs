using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CombatVariables : MonoBehaviour, Damageable
{
    public Stats stats;


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

    protected virtual void changeHpBar(float filled)
    {
        hpBar.fillAmount = filled;

    }

    protected virtual void ifHPzero()
    {
        Destroy(gameObject);

    }

    public virtual void weaponUpgrade() { }


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
                ifHPzero();
                return 0;
            }

            changeHpBar((float)hp / stats.hp);

            if (onHpChangedCallback != null) onHpChangedCallback.Invoke(receivedDmg * (-1));
            return hp;
        }
    }

    public int IncreaseHP(int amount)
    {
        if (hp.Equals(stats.hp)) return 0; // in case hp is full

        hp = Math.Min(stats.hp, hp + amount);
        //Debug.LogFormat("object {0} hp increased by {1}, current hp: {2}", gameObject.tag, amount, hp);
        changeHpBar((float)hp / stats.hp);
        createFloatingNumberText(transform.position, Color.green, amount.ToString());

        if (onHpChangedCallback != null) onHpChangedCallback.Invoke(amount);

        return hp;
    }

    protected virtual void initialize() { }

    void Start()
    {
        floatingNumberText = Resources.Load("FloatingNumberText") as GameObject;
        transform = GetComponent<Transform>();
        stats = GetComponent<Stats>();


        //Debug.LogFormat("total hp: {0}", stats.hp);
        dmgColor = new Dictionary<DamageType, Color>();
        dmgColor.Add(DamageType.Projectile, Color.red);
        dmgColor.Add(DamageType.Beam, Color.magenta);
        dmgColor.Add(DamageType.Physical, Color.gray);
        dmgColor.Add(DamageType.Default, Color.white);


        resistances = new Dictionary<DamageType, float>();
        resistances.Add(DamageType.Projectile, stats.projectileResistance);
        resistances.Add(DamageType.Beam, stats.beamResistance);
        resistances.Add(DamageType.Physical, stats.physicalResistance);
        resistances.Add(DamageType.Default, 0f);
        levelManager = LevelManager.instance;
        immune = false;

        StartCoroutine(sethp());

        initialize();
    }

    private IEnumerator sethp() { yield return new WaitForSeconds(0.2f); hp = stats.hp; } // since level modifier takes some time to set up


    IEnumerator stopStatus(StatusEffect status, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentlyAfflicted.Remove(status);
        activateDeactivateStatus(status, false, 0f);
    }

    protected virtual void activateDeactivateStatus(StatusEffect status, bool activate, float value) { }
    public void inflictStatus(StatusEffect status, float duration = 20f)
    {
        Debug.Log(gameObject.name + " getting inflicted by " + status.ToString());
        if (!currentlyAfflicted.Contains(status))
        {
            currentlyAfflicted.Add(status);
            StartCoroutine(stopStatus(status, duration));
            activateDeactivateStatus(status, true, duration);
        }
    }

    protected virtual void onDestroy() { }

    private void OnDestroy()
    {
        if (hp.Equals(0)) levelManager.increaseScore(stats.scoreValue);
        onDestroy();
    }

    protected virtual void handleStatBuff(StatBuff buff, float amount) { }
    public void permanentStatBuff(StatBuff buff, float amount)
    {
        handleStatBuff(buff, amount);
        stats.refreshOriginal();
    }

    public virtual void handleEquipement(Equipement eq) { }

    
}


public enum StatusEffect
{
    Stun,
    Slowdown,
    Speedup,
    DamageDecrease,
    DamageIncrease,
    Heal,
    HealOverTime,
    NoMovement,
    None,
}

public enum DamageType
{
    Beam,
    Physical,
    Projectile,
    Default
}

public enum StatBuff
{
    HP,
    Damage,
    MovementSpeed,
    RateOfFire,
    MagazineSize
}

