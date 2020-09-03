using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialWeaponScript : MonoBehaviour
{
    private int equipedWeaponID;

    private float cooldown;
    private float timeTillUse;
    private int dmgBase;
    private string dmgType;
    private float activeTime;
    private float activeFor;


    private LineRenderer lineRenderer;
    public Transform laserHitPoint;

    public GameObject invisible;

    private Stats stats;


    public void setParams(int dmgBase, string dmgType, float cooldown)
    {
        this.dmgBase = dmgBase;
        this.dmgType = dmgType;
        this.cooldown = cooldown;
    }

    // Start is called before the first frame update
    void Start()
    {

        stats = GetComponentInParent<Stats>();

        equipedWeaponID = 0;

        if (equipedWeaponID == 0)
        {
            //beam weapon
            //dmgBase = 30;
            //cooldown = 1f;
            timeTillUse = 0f;
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            lineRenderer.useWorldSpace = true;
            activeTime = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {


        timeTillUse -= Time.deltaTime;
        activeFor -= Time.deltaTime;
        if (timeTillUse <= 0 && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("I'ma fireing my lazer");
            timeTillUse = cooldown;
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
                    hit.collider.gameObject.GetComponent<CombatVariables>().DecreaseHP((int)Math.Round(dmgBase * stats.calculateFinalDmgModifier()), dmgType);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, laserHitPoint.position);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }
                else if (hit.collider.tag == "Terrain")
                {
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, laserHitPoint.position);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }
            }

            //this is a fat hitbox
            /*RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up);
            RaycastHit2D[] allHitL = Physics2D.RaycastAll(transform.position + transform.right*(-1*0.02f), transform.up);
            RaycastHit2D[] allHitR = Physics2D.RaycastAll(transform.position + transform.right * (1 * 0.02f), transform.up);
            RaycastHit2D[] allHitLL = Physics2D.RaycastAll(transform.position + transform.right * (-1 * 0.04f), transform.up);
            RaycastHit2D[] allHitRR = Physics2D.RaycastAll(transform.position + transform.right * (1 * 0.04f), transform.up);
            int i = 0;
            while (i<10)
            {
                //Debug.DrawLine(transform.position, hit.point);
                //laserHitPoint.position = allHit[i].point;
                //Debug.Log(allHit[i].collider.tag);
                if (allHit.Length>i && allHit[i].collider.tag == "Projectile")
                {
                    Destroy(allHit[i].collider.gameObject, 0f);
                }
                else if (allHitL.Length > i && allHitL[i].collider.tag == "Projectile")
                {
                    Destroy(allHitL[i].collider.gameObject, 0f);
                }
                else if (allHitR.Length > i && allHitR[i].collider.tag == "Projectile")
                {
                    Destroy(allHitR[i].collider.gameObject, 0f);
                }
                else if (allHitLL.Length > i && allHitLL[i].collider.tag == "Projectile")
                {
                    Destroy(allHitLL[i].collider.gameObject, 0f);
                }
                else if (allHitRR.Length > i && allHitRR[i].collider.tag == "Projectile")
                {
                    Destroy(allHitRR[i].collider.gameObject, 0f);
                }

                if (allHit.Length > i && allHit[i].collider.tag == "Enemy")
                {
                    allHit[i].collider.gameObject.GetComponent<CombatVariables>().DecreaseHP(dmgBase);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHit[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }

                else if (allHitL.Length > i && allHitL[i].collider.tag == "Enemy")
                {
                    allHitL[i].collider.gameObject.GetComponent<CombatVariables>().DecreaseHP(dmgBase);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHitL[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }

                else if (allHitR.Length > i && allHitR[i].collider.tag == "Enemy")
                {
                    allHitR[i].collider.gameObject.GetComponent<CombatVariables>().DecreaseHP(dmgBase);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHitR[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }

                else if (allHitRR.Length > i && allHitRR[i].collider.tag == "Enemy")
                {
                    allHitRR[i].collider.gameObject.GetComponent<CombatVariables>().DecreaseHP(dmgBase);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHitRR[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }

                else if (allHitLL.Length > i && allHitLL[i].collider.tag == "Enemy")
                {
                    allHitLL[i].collider.gameObject.GetComponent<CombatVariables>().DecreaseHP(dmgBase);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHitLL[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }



                if (allHit.Length > i && allHit[i].collider.tag == "Terrain")
                {
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHit[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }
                //Debug.DrawLine(transform.position, laserHitPoint.position);
                //Debug.DrawLine(transform.position + transform.right * (-1 * 0.2f), allHitL[i].point);
                //Debug.DrawLine(transform.position + transform.right * (1 * 0.2f), allHitR[i].point);
                i++;
                if (i >= 10)
                {
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, allHit[i].point);
                    lineRenderer.enabled = true;
                    activeFor = activeTime;
                    break;
                }
                //lineRenderer.enabled = true;
                //activeFor = activeTime;
            }*/




        }
        if (activeFor<=0)
        {
            lineRenderer.enabled = false;
        }
    }
}
