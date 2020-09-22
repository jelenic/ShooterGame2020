using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyModule : MonoBehaviour
{
    public float cooldown;
    public float remainingCooldown;
    public bool active;
    public bool cooldownActive;


    protected virtual void initialize() { }
    protected virtual void updateStart() { }
    protected virtual void updateEnd() { }
    protected virtual void activateModule() { }

    

    void Start()
    {
        remainingCooldown = 0f;
        initialize();
    }

    private void Update()
    {
        updateStart();

        if (cooldownActive)
        {
            remainingCooldown = Math.Max(0f, remainingCooldown - Time.deltaTime);
            if (remainingCooldown.Equals(0f)) cooldownActive = false;
        }

        updateEnd();
    }


    protected void activate()
    {
        if (remainingCooldown.Equals(0f))
        {
            active = true;
            remainingCooldown = cooldown;
            activateModule();
        }

    }
}
