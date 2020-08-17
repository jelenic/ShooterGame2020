using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFire : MonoBehaviour
{

    private float timetillfire;
    public Transform firepoint;
    private bool autofire;
    private Stats stats;
    private int activeWeapon;
    private GameObject w1;
    private GameObject w2;






    public GameObject Bullets;
    public GameObject MiniLaser;


    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        timetillfire = stats.rateOfFire;
        autofire = false;

        activeWeapon = 1;
        w1 = Bullets;
        w2 = MiniLaser;
        
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
            Debug.Log(stats.rateOfFire);
            Debug.Log(timetillfire.ToString());
            timetillfire = stats.rateOfFire;
        }

        if (Input.GetKeyDown("1"))
        {
            activeWeapon = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            activeWeapon = 2;
        }
    }

    void Shoot()
    {
        if (activeWeapon == 1)
        {
            Instantiate(w1, firepoint.position, firepoint.rotation).GetComponent<PlayerFiredBullet>().damageModifier = stats.calculateFinalDmgModifier();
        }
        else if (activeWeapon == 2)
        {
            Instantiate(w2, firepoint.position, firepoint.rotation).GetComponent<PlayerFiredBullet>().damageModifier = stats.calculateFinalDmgModifier();
        }
    }

}
