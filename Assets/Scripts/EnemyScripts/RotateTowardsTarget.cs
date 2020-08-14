﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotateTowardsTarget : MonoBehaviour
{
    private Stats stats;
    public Quaternion defaultRotation;
    public Transform target;
    public GameObject bullet;
    public float fireWait;

    // Use this for initialization
    void Start () {
        stats = GetComponent<Stats>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        

        fireWait = stats.rateOfFire;
        defaultRotation = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
        //TO DO add checkign line of fire for non homing projectiles, just copy from CustomAI and delete it there
        fireWait = Math.Max(0.0f, fireWait - Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= stats.range)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.speed * Time.deltaTime);
            if (fireWait == 0.0f)
            {
                Instantiate(bullet, transform.position + transform.up * 1.5f, transform.rotation).GetComponent<FiredProjectile>().damageModifier = stats.damageModifier;
                fireWait = stats.rateOfFire;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, stats.speed * Time.deltaTime);
        }
        
	}
}
