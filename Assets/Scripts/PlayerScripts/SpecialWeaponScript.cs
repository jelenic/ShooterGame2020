using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialWeaponScript : MonoBehaviour
{
    private int equipedWeaponID;

    private float cooldown;
    private float timeTillUse;
    private int dmgBase;
    private int dmgType;
    private float activeTime;
    private float activeFor;


    private LineRenderer lineRenderer;
    public Transform laserHitPoint;


    // Start is called before the first frame update
    void Start()
    {
        equipedWeaponID = 0;

        if (equipedWeaponID == 0)
        {
            //beam weapon
            dmgBase = 30;
            cooldown = 3f;
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
            Debug.Log("I'ma fireing my lazer");
            timeTillUse = cooldown;

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
            RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, transform.up);
            
            foreach(RaycastHit2D hit in allHit)
            {
                //Debug.DrawLine(transform.position, hit.point);
                laserHitPoint.position = hit.point;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, laserHitPoint.position);
                lineRenderer.enabled = true;
                activeFor = activeTime;
                Debug.Log("lazer hit " + hit.collider.tag);
                if (hit.collider.tag == "Projectile")
                {
                    Debug.Log("should Destroy");
                    Destroy(hit.collider.gameObject, 0f);

                }
                else if (hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<CombatVariables>().DecreaseHP(dmgBase);
                }
            }
            


        }
        if (activeFor<=0)
        {
            lineRenderer.enabled = false;
        }
    }
}
