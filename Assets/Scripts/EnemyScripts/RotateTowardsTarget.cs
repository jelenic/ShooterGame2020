using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotateTowardsTarget : MonoBehaviour
{
    public float speed;
    public float range;
    public Quaternion defaultRotation;
    public Transform target;
    public GameObject bullet;
    public float rateOfFire;
    public float fireWait;

    // Use this for initialization
    void Start () {
        speed = 10f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        range = 26f;
        rateOfFire = 0.5f;
        fireWait = rateOfFire;
        defaultRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        //TO DO add checkign line of fire for non homing projectiles, just copy from CustomAI and delete it there
        fireWait = Math.Max(0.0f, fireWait - Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= range)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            if (fireWait == 0.0f)
            {
                Instantiate(bullet, transform.position + transform.up * 1.5f, transform.rotation);
                fireWait = rateOfFire;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, speed * Time.deltaTime);
        }
        
	}
}
