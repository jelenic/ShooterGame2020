using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiredProjectile : MonoBehaviour
{
    public int projectileDamage;
    public DamageType projectileDamageType;
    public float lifeDuration;
    public GameObject explosion;
    private Transform transform;
    public float damageModifier;
    public float velocityModifier;
    public List<String> passThrough;
    public List<String> damageable;
    public List<String> destroyable;

    public virtual void Initialize()
    {
        
    }

    void Awake()
    {
        damageModifier = 1f;
        Destroy(gameObject, lifeDuration);
        transform = GetComponent<Transform>();
        passThrough = new List<string>();
        damageable = new List<string>();
        passThrough.Add("Item");
        //AudioManager.instance.PlayEffect("bullet3");

        Initialize();
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        //if (!LevelManager.instance.levelOver) AudioManager.instance.PlayEffect("explosion1");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        //Debug.LogFormat("kinematic bullet hit:{0}", hit.tag);
        if (!passThrough.Contains(hit.tag))
        {
            if (damageable.Contains(hit.tag)) hit.GetComponent<Damageable>().DecreaseHP((int)Math.Round(projectileDamage*damageModifier), projectileDamageType);
            if (destroyable.Contains(hit.tag))
            {
                Destroy(hit, 0f);
            }
            Destroy(gameObject, 0.0f);

        }
    }
}
