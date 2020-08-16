using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFire : MonoBehaviour
{

    private float timetillfire;
    public Transform firepoint;
    public GameObject Bullets;
    private bool autofire;
    private Stats stats;


    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        timetillfire = stats.rateOfFire;
        autofire = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        timetillfire -= Time.deltaTime;

        if (Input.GetKeyDown("space"))
        {
            if (autofire)
            {
                autofire = false;
            }
            else
            {
                autofire = true;
            }
        }

        if (timetillfire<=0 && autofire)
        {
            Shoot();
            timetillfire = stats.rateOfFire;
        }
    }

    void Shoot()
    {
        Instantiate(Bullets, firepoint.position, firepoint.rotation).GetComponent<BulletFire>().damageModifier = stats.calculateFinalDmgModifier();
    }

}
