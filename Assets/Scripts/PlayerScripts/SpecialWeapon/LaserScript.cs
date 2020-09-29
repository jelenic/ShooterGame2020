using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserScript : SpecialWeaponScript
{
    
    private float activeTime;
    private float activeFor;
    public LayerMask affectedLayers;

    private LineRenderer lineRenderer;
    public Transform laserHitPoint;

    public GameObject invisible;

    public GameObject chargeAnimation;
    private GameObject chargeAnimationInstance;
    private ChargeController chargeController;

    float range;

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
        float reachedCharge = calculateCharge();
        lineRenderer.widthMultiplier = reachedCharge / 20f;

        range = Mathf.Clamp(15f * reachedCharge, 10f, 100f);
        activeFor = reachedCharge / 3f;


        laserHitPoint.position = transform.position + transform.up * range;

        StartCoroutine(doDamage(reachedCharge));
    }

    private IEnumerator doDamage(float reachedCharge)
    {
        while (activeFor > 0)
        {
            Instantiate(invisible, transform.position, transform.rotation);

            RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up, range, affectedLayers);
            foreach (RaycastHit2D hit in allHit)
            {
                Debug.Log("laser hit " + hit.collider.tag);
                if (hit.collider.tag == "Projectile")
                {
                    Destroy(hit.collider.gameObject, 0f);
                }
                else if (hit.collider.tag == "Enemy")
                {
                    int target_hp = hit.collider.gameObject.GetComponent<Damageable>().DecreaseHP((int)Math.Round(dmgBase * reachedCharge * 0.1f * stats.calculateFinalDmgModifier()), dmgType);
                    specialEffect(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("EnemyShield"))
                {
                    laserHitPoint.position = hit.point;
                    break;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    protected override void updateFinish()
    {
        base.updateFinish();
        if (activeFor <= 0)
        {
            lineRenderer.enabled = false;
        } else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, laserHitPoint.position);
            lineRenderer.enabled = true;
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
