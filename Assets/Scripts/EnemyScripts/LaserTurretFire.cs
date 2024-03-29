﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretFire : ChargeTurret
{
    public LayerMask ignoredLayers;


    public GameObject chargeAnimation;
    private GameObject chargeAnimationInstance;
    private ChargeController chargeController;

    private float activeTime;
    private float activeFor;

    private float calculatedMissChance;

    private LineRenderer lineRenderer;
    private Vector3 laserHitPoint;

    protected override void initialize()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        activeTime = 0.2f;
        laserHitPoint = transform.position + transform.up * 1.5f;
    }

    protected override void updateStart()
    {
        activeFor -= Time.deltaTime;
    }

    protected override void stuff()
    {
        base.stuff();
        lineRenderer.widthMultiplier = calculateCharge() / 20f;

        RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up, 40f, ~ignoredLayers);
        if (allHit.Length.Equals(0))
        {
            lineRenderer.SetPosition(0, transform.position + transform.up * 1.5f);
            lineRenderer.SetPosition(1, transform.position + transform.up * 40f);
            lineRenderer.enabled = true;
            activeFor = activeTime;
            return;
        }
        foreach (RaycastHit2D hit in allHit)
        {
            laserHitPoint = hit.point;
            if (hit.collider.tag == "PlayerProjectile")
            {
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Damageable>().DecreaseHP((int)Mathf.Round(dmgBase * calculateCharge() * stats.calculateFinalDmgModifier()), dmgType);
                lineRenderer.SetPosition(0, transform.position + transform.up * 1.5f);
                lineRenderer.SetPosition(1, laserHitPoint);
                lineRenderer.enabled = true;
                activeFor = activeTime;
                break;
            }
            else if (hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("Shield"))
            {
                lineRenderer.SetPosition(0, transform.position + transform.up * 1.5f);
                lineRenderer.SetPosition(1, laserHitPoint);
                lineRenderer.enabled = true;
                activeFor = activeTime;
                break;
            }
        }

    }

    protected override void updateEnd()
    {
        if (activeFor <= 0)
        {
            lineRenderer.enabled = false;
        }
    }


    protected override void onChargeBegin()
    {
        chargeAnimationInstance = Instantiate(chargeAnimation, transform.position + transform.up * 1.5f, transform.rotation);
        chargeController = chargeAnimationInstance.GetComponent<ChargeController>();
    }

    protected override void onChargeChange()
    {
        chargeController.refresh(calculateCharge() / 30f, transform.position + transform.up * 1.5f);
        if (inDelay)
        {
            lineRenderer.SetPosition(0, transform.position + transform.up * 1.5f);
            lineRenderer.SetPosition(1, transform.position + transform.up * 100f);
        }
    }

    protected override void onChargeEnd()
    {
        Destroy(chargeAnimationInstance);
        chargeController = null;


    }

    protected override void onFireDelay()
    {
        cv.inflictStatus(StatusEffect.NoMovement, fireDelay);
        
        lineRenderer.widthMultiplier = calculateCharge() / 30f;
        lineRenderer.SetPosition(0, transform.position + transform.up * 1.5f);
        lineRenderer.SetPosition(1, transform.position + transform.up * 40f);
        lineRenderer.enabled = true;
        activeFor = fireDelay;

    }

    private void OnDestroy()
    {
        Destroy(chargeAnimationInstance);
    }



}
