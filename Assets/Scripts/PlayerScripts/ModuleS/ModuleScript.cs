using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleScript : MonoBehaviour
{
    //defence, movement, ofense
    private enum type{WithSprite, WithParticles, NoEffect};
    [SerializeField] protected float coooldown;
    [SerializeField] protected float duration;

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

    public void setParams(float cooldown, float duration)
    {
        if (cooldown >= 0) coooldown = cooldown;
        if (duration >= 0) this.duration = duration;
    }

    void Start()
    {
        remainingCooldown = coooldown;
        active = false;
        ship = GameObject.FindGameObjectWithTag("Player");
        initialize();

    }



    // Update is called once per frame
    void Update()
    {

        remainingCooldown = Math.Max(0f, remainingCooldown - Time.deltaTime);
        remainingTime -= Time.deltaTime;
        if (remainingCooldown <= 0 && Input.GetMouseButtonDown(1))
        {
            remainingCooldown = coooldown + duration;
            remainingTime = duration;
            activeAction();
            active = true;

        }
        if (remainingTime <= 0 && active)
        {
            inactiveAction();
            active = false;
        }

        if (OnCooldownChangedCallback != null)
        {
            OnCooldownChangedCallback.Invoke((coooldown - remainingCooldown) / coooldown);
        }
    }


}
