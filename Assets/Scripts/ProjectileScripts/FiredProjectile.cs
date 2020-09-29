using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiredProjectile : MonoBehaviour
{
    public string weaponName;
    public int projectileDamage;
    public float projectileSpeed;
    public DamageType projectileDamageType;
    public float lifeDuration;
    public GameObject explosion;
    private Transform transform;
    public float damageModifier;
    public float velocityModifier;
    public List<String> passThrough;
    public List<String> damageable;
    public List<String> destroyable;

    public int destroyableNumber;
    public int destroyed;

    protected LevelManager levelManager;

    public virtual void Initialize()
    {
        
    }

    void Awake()
    {
        levelManager = LevelManager.instance;

        destroyableNumber = Mathf.Max(destroyableNumber, 1);
        destroyed = 0;
        damageModifier = 1f;
        transform = GetComponent<Transform>();
        passThrough = new List<string>();
        damageable = new List<string>();
        passThrough.Add("Item");
        destroyable.Add("Projectile");
        destroyable.Add("PlayerProjectile");

        //AudioManager.instance.PlayEffect("bullet3");

        Initialize();
        Destroy(gameObject, lifeDuration);
    }

    private void OnDestroy()
    {
        if (Time.timeScale != 0f) Instantiate(explosion, transform.position, transform.rotation);
        //if (!LevelManager.instance.levelOver) AudioManager.instance.PlayEffect("explosion1");

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        //Debug.LogFormat("kinematic bullet hit:{0}", hit.tag);
        if (!passThrough.Contains(hit.tag))
        {
            activate(hit);
        }
    }

    protected virtual void activate(GameObject hit)
    {
        if (destroyable.Contains(hit.tag))
        {
            //Destroy(hit, 0f);
            if (++destroyed < destroyableNumber)
            {
                Debug.Log(destroyed + " destroyed, remaining: " + (destroyableNumber - destroyed));
                return;
            }
        }
        if (damageable.Contains(hit.tag))
        {
            int target_hp = hit.GetComponent<Damageable>().DecreaseHP((int)Math.Round(projectileDamage * damageModifier), projectileDamageType);
            if (hit.CompareTag("Enemy") && target_hp.Equals(0) && levelManager != null) levelManager.weaponKill(weaponName);
        }
            
        Destroy(gameObject, 0.0f);
    } 
}
