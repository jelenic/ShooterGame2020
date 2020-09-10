using System;
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


    public void setParams(int dmgBase, string dmgType, float cooldown)
    {
        this.dmgBase = dmgBase;
        this.dmgType = dmgType;
        this.cooldown = cooldown;
    }

    // Start is called before the first frame update
    void Start()
    {

        stats = GetComponentInParent<Stats>();

        timeTillUse = 0f;
        clicked = false;
        active = false;

        if (Application.platform == RuntimePlatform.Android || true)
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
        if (timeTillUse <= 0)
        {
            timeTillUse = cooldown;
            stuff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeTillUse -= Time.deltaTime;

        updateStart();


        if (/*Input.GetMouseButtonDown(0)*/ clicked)
        {
            clicked = false;
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
}
