using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ModuleScript : MonoBehaviour, IActiveOrCharging
{
    //defence, movement, ofense
    protected Module module;

    //this part is for btns
    protected Button moduleBtn;
    private bool clicked;

    public bool android;

    // Start is called before the first frame update
    protected float remainingCooldown;
    protected float remainingTime;
    protected GameObject ship;
    protected bool active;

    public delegate void OnCooldownChanged(float filled);
    public OnCooldownChanged OnCooldownChangedCallback;

    public delegate void OnActivated();
    public OnActivated OnActivatedCallback;

    protected virtual void initialize() { }
    protected virtual void activeAction() { }
    protected virtual void inactiveAction() { }
    protected virtual void activeUpdate() { }


    public void setParams(Module module)
    {
        this.module = module;
    }

    void Start()
    {
        android = false;
        remainingCooldown = module.cooldown / 10f;
        active = false;
        clicked = false;
        ship = GameObject.FindGameObjectWithTag("Player");
        if (Application.platform == RuntimePlatform.Android || android)
        {
            GameObject moduleBtnn = GameObject.Find("ModuleBtn");
            moduleBtn = moduleBtnn.GetComponent<Button>();
            moduleBtn.onClick.AddListener(useModule);
        }
        initialize();

    }



    // Update is called once per frame
    void Update()
    {
        if (active) activeUpdate();
        remainingCooldown = Math.Max(0f, remainingCooldown - Time.deltaTime);
        remainingTime -= Time.deltaTime;
        if (remainingCooldown <= 0 && (Input.GetMouseButtonDown(0) || clicked))
        {
            remainingCooldown = module.cooldown + module.duration;
            remainingTime = module.duration;
            activeAction();
            active = true;
            clicked = false;
            if (OnActivatedCallback != null) OnActivatedCallback.Invoke();
            //moduleBtn.onClick.RemoveListener(useModule);

        }
        if (remainingTime <= 0 && active)
        {
            inactiveAction();
            active = false;
            //moduleBtn.onClick.AddListener(useModule);
        }

        if (OnCooldownChangedCallback != null)
        {
            OnCooldownChangedCallback.Invoke((module.cooldown - remainingCooldown) / module.cooldown);
        }
    }

    public void useModule()
    {
        if (!active && remainingCooldown<=0)
        {
            clicked = true;
        }
    }

    public bool isActiveOrCharging()
    {
        return active;
    }


}


