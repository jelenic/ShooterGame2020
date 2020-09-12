using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotateTowardsTarget : MonoBehaviour
{
    protected Stats stats;
    public Quaternion defaultRotation;
    public Transform target;
    public GameObject bullet;
    public float fireWait;

    // Use this for initialization
    protected virtual void Initialize()
    {
        stats = GetComponent<Stats>();
    }
    protected virtual void fire()
    {
        FiredProjectile fp = Instantiate(bullet, transform.position + transform.up * 1.5f, transform.rotation).GetComponent<FiredProjectile>();
        fp.damageModifier = stats.calculateFinalDmgModifier();

        fp.velocityModifier = stats.projectileVelocityModifier;
        fireWait = stats.rateOfFire;
    }

    void Start () {
        Initialize();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        fireWait = 0.5f;
        defaultRotation = transform.rotation;

	}

    protected virtual void updateStart() { }
    protected virtual void updateEnd() { }
	
	// Update is called once per frame
	void Update () {
        updateStart();
        //TO DO add checkign line of fire for non homing projectiles, just copy from CustomAI and delete it there
        fireWait = Math.Max(0.0f, fireWait - Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= stats.range)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.turretRotationSpeed * Time.deltaTime);
            if (fireWait == 0.0f)
            {
                fire();
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, stats.turretRotationSpeed * Time.deltaTime);
        }
        updateEnd();
        
	}
}
