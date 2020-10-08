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

    protected Vector2 speedConstant;

    public List<String> damageable;
    public List<String> destroyable;

    public int destroyableNumber;
    protected int destroyed;

    protected LevelManager levelManager;

    public virtual void Initialize()
    {
        
    }

    void Awake()
    {
        levelManager = LevelManager.instance;
        transform = GetComponent<Transform>();

        destroyableNumber = Mathf.Max(destroyableNumber, 1);
        destroyed = 0;
        damageModifier = 1f;

        //AudioManager.instance.PlayEffect("bullet3");

        speedConstant = Vector2.up * projectileSpeed;


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
        activate(hit);
    }

    protected virtual void activate(GameObject hit)
    {
        if (destroyable.Contains(hit.tag))
        {
            //Destroy(hit, 0f);
            if (++destroyed < destroyableNumber)
            {
                //Debug.Log(destroyed + " destroyed, remaining: " + (destroyableNumber - destroyed));
                return;
            }
        }
        else if (damageable.Contains(hit.tag))
        {
            int target_hp = hit.GetComponent<Damageable>().DecreaseHP((int)Math.Round(projectileDamage * damageModifier), projectileDamageType);
            if (hit.CompareTag("Enemy") && target_hp.Equals(0) && levelManager != null) levelManager.weaponKill(weaponName);
        }
        else
        {
            IShield im = hit.GetComponent<IShield>();
            if (im != null)
            {
                if (im.getShieldType().Equals(ShieldType.DeflectorShield))
                {
                    Deflect();
                    return;
                }
                else if (im.getShieldType().Equals(ShieldType.HPShield))
                {
                    hit.GetComponent<Damageable>().DecreaseHP((int)Math.Round(projectileDamage * damageModifier), projectileDamageType);
                }
            }
        }
            
        Destroy(gameObject);
    }


    public virtual void Deflect() { }
}
