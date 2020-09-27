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
    public bool inCooldown;
    public Transform parentTransform;



    protected virtual void initialize() { }
    protected virtual void updateStart() { }
    protected virtual void updateEnd() { }
    protected virtual void activateModule() { }

    

    void Start()
    {
        remainingCooldown = 0f;
        initialize();
    }

    protected IEnumerator cooldownCoroutine()
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldown);
        inCooldown = false;

    }

    private void FixedUpdate()
    {
        updateStart();

        updateEnd();
    }


    protected void activate()
    {
        if (!inCooldown)
        {
            active = true;
            activateModule();
        }

    }
}
