using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ModuleScript : MonoBehaviour
{
    //defence, movement, ofense
    private enum type{WithSprite, WithParticles, NoEffect};
    [SerializeField] protected float coooldown;
    [SerializeField] protected float duration;

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

    protected virtual void initialize() { }
    protected virtual void activeAction() { }
    protected virtual void inactiveAction() { }
    protected virtual void activeUpdate() { }

    public void setParams(float cooldown, float duration)
    {
        if (cooldown >= 0) coooldown = cooldown;
        if (duration >= 0) this.duration = duration;
    }

    void Start()
    {
        android = false;
        remainingCooldown = coooldown / 10f;
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
        if (remainingCooldown <= 0 && (Input.GetMouseButtonDown(1) || clicked))
        {
            remainingCooldown = coooldown + duration;
            remainingTime = duration;
            activeAction();
            active = true;
            clicked = false;
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
            OnCooldownChangedCallback.Invoke((coooldown - remainingCooldown) / coooldown);
        }
    }

    public void useModule()
    {
        if (!active && remainingCooldown<=0)
        {
            clicked = true;
        }
    }


}
