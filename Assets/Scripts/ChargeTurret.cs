using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTurret : MonoBehaviour
{
    public bool isCharging;
    public float chargeLevel;
    public float chargeSpeed;
    public float minCharge;
    public float maxCharge;
    public float chargeHold;
    public float currentChargeHold;
    public float cooldown;
    public float timeTillUse;
    public float chanceToMiss;
    public int dmgBase;
    public DamageType dmgType;
    public bool parentStats;

    protected Transform transform;
    protected Transform target;

    protected Stats stats;


    protected virtual void initialize() { }
    protected virtual void updateStart() { }
    protected virtual void updateEnd() { }
    protected virtual void stuff() { }
    protected virtual void onChargeBegin() { }
    protected virtual void onChargeChange() { }
    protected virtual void onChargeEnd() { }

  
    protected float calculateCharge()
    {
        return Mathf.Min(maxCharge, minCharge + chargeLevel);
    }

    private void doStuff()
    {
        //Debug.Log("charged up " + chargeLevel);
        timeTillUse = cooldown;
        stuff();
        currentChargeHold = chargeHold;
        chargeLevel = 0f;
        onChargeEnd();
        isCharging = false;

    }

    private void Awake()
    {
        stats = parentStats ? gameObject.GetComponentInParent<Stats>() : GetComponent<Stats>();
        transform = GetComponent<Transform>();
        timeTillUse = 0.5f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentChargeHold = chargeHold;
        initialize();
    }

    

    private void FixedUpdate()
    {
        updateStart();
        timeTillUse = Mathf.Max(0f, timeTillUse - Time.deltaTime);

        if (timeTillUse.Equals(0f))
        {
            if (target != null && Vector2.Distance(transform.position, target.position) <= stats.range) // if target(player) in range
            {
                if (!isCharging) onChargeBegin();
                isCharging = true;
                chargeLevel = Mathf.Min(maxCharge, chargeLevel + Time.deltaTime * chargeSpeed * (stats.og.rateOfFire / stats.rateOfFire));
                onChargeChange();

                if (calculateCharge().Equals(maxCharge))
                {
                    currentChargeHold = Mathf.Max(0f, currentChargeHold - Time.deltaTime);
                    if (currentChargeHold.Equals(0f)) doStuff();
                    else
                    {
                        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * 1.5f, transform.up);
                        if (hit.collider.CompareTag("Player"))
                        {
                            doStuff();
                        }
                    }
                }
            }
            else // player not in range
            {
                if (isCharging)
                {
                    chargeLevel = Mathf.Max(0f, chargeLevel - Time.deltaTime * chargeSpeed * (stats.og.rateOfFire / stats.rateOfFire));
                    onChargeChange();
                    currentChargeHold = chargeHold;

                    if (chargeLevel.Equals(0f))
                    {
                        onChargeEnd();
                        isCharging = false;
                    }
                }
            }
            
        }
        updateEnd();

    }

}
