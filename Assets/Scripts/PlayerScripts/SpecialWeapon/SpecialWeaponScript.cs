﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class SpecialWeaponScript : MonoBehaviour
{
    protected float cooldown;
    protected float timeTillUse;
    protected int dmgBase;
    protected string dmgType;

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



    public void setParams(SpecialWeapon sw)
    {
        this.dmgBase = sw.damageBase;
        this.dmgType = sw.damageType;
        this.cooldown = sw.cooldown;
        this.chargeSpeed = sw.chargeSpeed;
        this.minCharge = sw.minCharge;
        this.maxCharge = sw.maxCharge;
    }

    // Start is called before the first frame update
    void Start()
    {

        stats = GetComponentInParent<Stats>();

        timeTillUse = 0f;
        

        if (Application.platform == RuntimePlatform.Android || android)
        {
            GameObject specialWBtnn = GameObject.Find("SpecialAttackBtn");
            specialWBtn = specialWBtnn.GetComponent<Button>();
            specialWBtn.onClick.AddListener(useWeapon);
        }

        initialize();
    }

    protected virtual void initialize() { }
    protected virtual void updateStart() { }
    protected virtual void updateFinish() { }
    protected virtual void stuff() { }
    private void doStuff()
    {

        Debug.Log("charged up " + chargeLevel);
        if (timeTillUse <= 0)
        {
            timeTillUse = cooldown;
            stuff();
            chargeLevel = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeTillUse -= Time.deltaTime;

        updateStart();

        if (timeTillUse <= 0f && (Input.GetMouseButton(0) || clicked))
        {
            isCharging = true;
            chargeLevel += Time.deltaTime * chargeSpeed;
            clicked = false;
        }
        else if(isCharging && !Input.GetMouseButton(0) && !clicked)
        {

            isCharging = false;
            active = true;
            doStuff();
            active = false;

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
}
