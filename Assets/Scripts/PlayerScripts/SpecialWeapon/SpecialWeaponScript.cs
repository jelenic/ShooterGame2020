using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class SpecialWeaponScript : MonoBehaviour
{
    protected float cooldown;
    protected float timeTillUse;
    protected float calculatedCooldown;
    protected int dmgBase;
    protected DamageType dmgType;

    protected Stats stats;

    //this part is for btns
    protected Button specialWBtn;
    private bool clicked;
    protected bool active;

    public bool android;

    public bool isCharging;
    public float chargeLevel;
    protected float chargeSpeed;
    protected float minCharge;
    protected float maxCharge;
    protected StatusEffect statusEffect;
    protected float statusDuration;

    protected LevelManager levelManager;
    protected string specialWeaponName;

    protected Transform transform;

    public delegate void OnCooldownChanged(float filled);
    public OnCooldownChanged OnCooldownChangedCallback;
    
    public delegate void OnFired();
    public OnFired OnFiredCallback;

    public delegate bool CanFire();
    private CanFire canFire;

    private bool freeFire()
    {
        return true;
    }

    public void setParams(SpecialWeapon sw, CanFire canFire = null )
    {
        if (canFire == null) this.canFire = freeFire;
        else this.canFire = canFire;

        this.dmgBase = sw.damageBase;
        this.dmgType = sw.damageType;
        this.cooldown = sw.cooldown;
        this.chargeSpeed = sw.chargeSpeed;
        this.minCharge = sw.minCharge;
        this.maxCharge = sw.maxCharge;
        this.statusEffect = sw.statusEffect;
        this.statusDuration = sw.statusEffectDuration;
        this.specialWeaponName = sw.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        stats = GetComponentInParent<Stats>();

        timeTillUse = 0f;

        transform = GetComponent<Transform>();
        

        if (Application.platform == RuntimePlatform.Android || android)
        {
            android = true;
            GameObject specialWBtnn = GameObject.Find("SpecialAttackBtn");
            specialWBtn = specialWBtnn.GetComponent<Button>();
            specialWBtn.onClick.AddListener(useWeapon);
        }

        initialize();
    }

    protected virtual void initialize() { }
    protected virtual void updateStart() { }
    protected virtual void updateFinish() { }
    protected virtual void stuff(float modifier = 1f) { }
    protected virtual void onChargeBegin() { }
    protected virtual void onChargeChange() { }
    protected virtual void onChargeEnd() { }
    private void doStuff()
    {

        //Debug.Log("charged up " + chargeLevel);
        calculatedCooldown = timeTillUse = Mathf.Clamp(cooldown * (-1f + Mathf.Pow(1.4f, calculateCharge())), cooldown / 10f, cooldown * 2f);
        stuff();

        if (OnFiredCallback != null)
        {
            OnFiredCallback.Invoke();
        }

        chargeLevel = 0f;
        onChargeEnd();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeTillUse -= Time.deltaTime;

        if (OnCooldownChangedCallback != null)
        {
            OnCooldownChangedCallback.Invoke((calculatedCooldown - timeTillUse) / calculatedCooldown);
        }

        updateStart();

        if (canFire() && timeTillUse <= 0f && (Input.GetMouseButton(0) || clicked))
        {
            if (!isCharging) onChargeBegin();
            isCharging = true;
            onChargeChange();
            chargeLevel += Time.deltaTime * chargeSpeed;
            clicked = false;
        }
        else if(isCharging && !Input.GetMouseButton(0) && !clicked)
        {
            onChargeEnd();
            isCharging = false;
            active = true;
            doStuff();
        }


        updateFinish();
    }

    void useWeapon()
    {
        if (!active && timeTillUse<=0)
        {
            clicked = true;
        }
    }

    protected float calculateCharge()
    {
        return Mathf.Min(maxCharge, minCharge + chargeLevel);
    }



    public bool isActiveOrCharging()
    {
        return (isCharging || active);
    }
}
