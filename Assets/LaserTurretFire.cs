using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretFire : ChargeTurret
{
    public GameObject chargeAnimation;
    private GameObject chargeAnimationInstance;
    private ChargeController chargeController;

    private float activeTime;
    private float activeFor;

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

        RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up + transform.right * Random.Range(-chanceToMiss, chanceToMiss)); // moves the aim a chanceToMiss to left or right
        foreach (RaycastHit2D hit in allHit)
        {
            laserHitPoint = hit.point;
            if (hit.collider.tag == "PlayerProjectile")
            {
                Destroy(hit.collider.gameObject, 0f);

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
    }

    protected override void onChargeEnd()
    {
        Destroy(chargeAnimationInstance);
        chargeController = null;
    }

    private void OnDestroy()
    {
        Destroy(chargeAnimationInstance);
    }



}
