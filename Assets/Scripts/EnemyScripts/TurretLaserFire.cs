using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserFire : RotateTowardsTarget
{
    LineRenderer lineRenderer;
    GameObject charging;
    Vector3 laserHitPoint;
    Vector3 charged_position;

    public bool firing_laser;
    public float activeTime;
    public float activeFor;

    protected override void Initialize()
    {
        base.Initialize();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        activeTime = 0.2f;
    }
    protected override void fire()
    {
        Debug.Log("turret laser firing");
        charging = Instantiate(bullet, transform.position + transform.up * 1.5f, transform.rotation);
        charging.transform.parent = transform;
        charging.GetComponent<Charger>().onChargedCallback += charged;
        fireWait = stats.rateOfFire;
    }

    private void charged(Vector3 position)
    {
        firing_laser = true;
        charged_position = position;
        activeFor = activeTime;
    }

    protected override void updateStart()
    {
        if (firing_laser)
        {
            

            RaycastHit2D[] allHit = Physics2D.RaycastAll(charged_position, transform.up);
            foreach (RaycastHit2D hit in allHit)
            {
                //Debug.DrawLine(transform.position, hit.point);
                laserHitPoint = hit.point;
                //lineRenderer.enabled = true;
                //activeFor = activeTime;
                Debug.Log("lazer hit " + hit.collider.tag);
                if (hit.collider.tag == "PlayerProjectile")
                {
                    //Debug.Log("should Destroy");
                    Destroy(hit.collider.gameObject, 0f);

                }
                else if (hit.collider.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<Damageable>().DecreaseHP((int)Mathf.Round(5 * stats.calculateFinalDmgModifier()), "beam");
                    lineRenderer.SetPosition(0, charged_position);
                    lineRenderer.SetPosition(1, laserHitPoint);
                    lineRenderer.enabled = true;
                    break;
                }
                else if (hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("Shield"))
                {
                    lineRenderer.SetPosition(0, charged_position);
                    lineRenderer.SetPosition(1, laserHitPoint);
                    lineRenderer.enabled = true;
                    break;
                }
            }
        }
    }


    protected override void updateEnd()
    {
        activeFor -= Time.deltaTime;
        base.updateEnd();
        if (activeFor <= 0)
        {
            lineRenderer.enabled = false;
            firing_laser = false;
        }
    }


}
