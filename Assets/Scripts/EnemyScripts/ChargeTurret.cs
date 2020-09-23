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
    public float fireDelay;
    public float currentChargeHold;
    public float cooldown;
    public float timeTillUse;
    public float chanceToMiss;
    public int dmgBase;
    public DamageType dmgType;
    public bool parentStats;
    public bool inDelay;

    protected Transform transform;
    protected Transform target;

    protected Stats stats;
    protected CombatVariables cv;


    protected virtual void initialize() { }
    protected virtual void updateStart() { }
    protected virtual void updateEnd() { }
    protected virtual void stuff() { }
    protected virtual void onChargeBegin() { }
    protected virtual void onChargeChange() { }
    protected virtual void onChargeEnd() { }
    protected virtual void onFireDelay() { }

  
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
        cv = parentStats ? gameObject.GetComponentInParent<CombatVariables>() : GetComponent<CombatVariables>();
        transform = GetComponent<Transform>();
        timeTillUse = 0.5f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentChargeHold = chargeHold;
        initialize();
    }

    private IEnumerator delayFire()
    {
        inDelay = true;
        onFireDelay();
        yield return new WaitForSeconds(fireDelay);
        doStuff();
        inDelay = false;

    }



    private void FixedUpdate()
    {
        updateStart();
        timeTillUse = Mathf.Max(0f, timeTillUse - Time.deltaTime);

        if (timeTillUse.Equals(0f))
        {
            if (isCharging) onChargeChange();
            if (inDelay) return;
            if (target != null && Vector2.Distance(transform.position, target.position) <= stats.range) // if target(player) in range
            {
                if (!isCharging) onChargeBegin();
                isCharging = true;
                chargeLevel = Mathf.Min(maxCharge, chargeLevel + Time.deltaTime * chargeSpeed * (stats.og.rateOfFire / stats.rateOfFire));
                

                if (calculateCharge().Equals(maxCharge))
                {
                    currentChargeHold = Mathf.Max(0f, currentChargeHold - Time.deltaTime);
                    if (currentChargeHold.Equals(0f)) StartCoroutine(delayFire());
                    else
                    {
                        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * 1.5f, transform.up);
                        if (hit.collider.CompareTag("Player"))
                        {
                            StartCoroutine(delayFire());
                        }
                    }
                }
            }
            else // player not in range
            {
                if (isCharging)
                {
                    chargeLevel = Mathf.Max(0f, chargeLevel - Time.deltaTime * chargeSpeed * (stats.og.rateOfFire / stats.rateOfFire));
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
