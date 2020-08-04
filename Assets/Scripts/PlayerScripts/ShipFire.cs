using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFire : MonoBehaviour
{

    private float timetillfire;
    public Transform firepoint;
    public GameObject Bullets;
    private bool autofire;


    // Start is called before the first frame update
    void Start()
    {
        timetillfire = 0.2f;
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
            timetillfire = 0.2f;
        }
    }

    void Shoot()
    {
        Instantiate(Bullets, firepoint.position, firepoint.rotation);
    }
}
