﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiredProjectile : MonoBehaviour
{
    public int projectileDamage;
    public float lifeDuration;
    public GameObject explosion;
    private Transform transform;
    public float damageModifier;

    public virtual void Initialize()
    {
        
    }

    void Awake()
    {
        damageModifier = 1f;
        projectileDamage = 10;
        lifeDuration = 5f;
        Destroy(gameObject, lifeDuration);
        transform = GetComponent<Transform>();
        Initialize();
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        //Debug.LogFormat("kinematic bullet hit:{0}", hit.tag);
        if (hit.tag != "Projectile" && hit.tag != "Enemy" && hit.tag != "Spawner")
        {
            if (hit.tag == "Player") hit.GetComponent<CombatVariables>().DecreaseHP((int)Math.Round(projectileDamage*damageModifier), "projectile");
            Destroy(gameObject, 0.0f);

        }
    }
}