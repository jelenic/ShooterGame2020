using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserScript : SpecialWeaponScript
{
    
    private float activeTime;
    private float activeFor;

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

    protected override void stuff()
    {
        base.stuff();

        lineRenderer.widthMultiplier = calculateCharge() / 20f;
        //Debug.Log("I'ma fireing my lazer");
        Instantiate(invisible, transform.position, transform.rotation);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);


        //this is a slim hitbox
        RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up);
        foreach (RaycastHit2D hit in allHit)
        {
            //Debug.DrawLine(transform.position, hit.point);
            laserHitPoint.position = hit.point;
            //lineRenderer.enabled = true;
            //activeFor = activeTime;
            //Debug.Log("lazer hit " + hit.collider.tag);
            if (hit.collider.tag == "Projectile")
            {
                //Debug.Log("should Destroy");
                Destroy(hit.collider.gameObject, 0f);

            }
            else if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Damageable>().DecreaseHP((int)Math.Round(dmgBase * calculateCharge() * stats.calculateFinalDmgModifier()), dmgType);
                specialEffect(hit.collider.gameObject);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, laserHitPoint.position);
                lineRenderer.enabled = true;
                activeFor = activeTime;
                break;
            }
            else if (hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("EnemyShield"))
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, laserHitPoint.position);
                lineRenderer.enabled = true;
                activeFor = activeTime;
                break;
            }
        }

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
