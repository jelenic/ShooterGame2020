using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserScript : SpecialWeaponScript
{
    
    private float activeTime;
    private float activeFor;
    public LayerMask ignoredLayers;

    private LineRenderer lineRenderer;
    public Transform laserHitPoint;

    public GameObject invisible;

    public GameObject chargeAnimation;
    private GameObject chargeAnimationInstance;
    private ChargeController chargeController;


    protected override void initialize()
    {
        base.initialize();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        activeTime = 0.2f;
    }

    protected override void updateStart()
    {
        base.updateStart();
        activeFor -= Time.deltaTime;
    }

    protected virtual void specialEffect(GameObject affected) { }

    protected override void stuff(float modifier = 1f)
    {
        lineRenderer.widthMultiplier = calculateCharge() / 20f;
        Instantiate(invisible, transform.position, transform.rotation);

        laserHitPoint.position = transform.position + transform.up * 50f;
        RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up, 50f, ~ignoredLayers);
        foreach (RaycastHit2D hit in allHit)
        {
            Debug.Log("laser hit " + hit.collider.tag);
            laserHitPoint.position = hit.point;
            if (hit.collider.tag == "Projectile")
            {
                Destroy(hit.collider.gameObject, 0f);
            }
            else if (hit.collider.tag == "Enemy")
            {
                int target_hp = hit.collider.gameObject.GetComponent<Damageable>().DecreaseHP((int)Math.Round(modifier * dmgBase * calculateCharge() * stats.calculateFinalDmgModifier()), dmgType);
                specialEffect(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("EnemyShield"))
            {
                break;
            }
        }
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, laserHitPoint.position);
        lineRenderer.enabled = true;
        activeFor = activeTime;

    }

    protected override void updateFinish()
    {
        base.updateFinish();
        if (activeFor <= 0)
        {
            lineRenderer.enabled = false;
        }
    }

    protected override void onChargeBegin()
    {
        chargeAnimationInstance = Instantiate(chargeAnimation, transform.position + transform.up, transform.rotation);
        chargeController = chargeAnimationInstance.GetComponent<ChargeController>();
    }

    protected override void onChargeChange()
    {
        chargeController.refresh(calculateCharge() / 30f, transform.position + transform.up);

    }

    protected override void onChargeEnd()
    {
        Destroy(chargeAnimationInstance);
        chargeController = null;
    }

}
