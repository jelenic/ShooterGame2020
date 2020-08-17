using System;
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
    public List<String> passThrough;
    public List<String> damageable;
    public List<String> destroyable;

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
        passThrough = new List<string>();
        damageable = new List<string>();
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
        if (!passThrough.Contains(hit.tag))
        {
            if (damageable.Contains(hit.tag)) hit.GetComponent<CombatVariables>().DecreaseHP((int)Math.Round(projectileDamage*damageModifier), "projectile");
            if (destroyable.Contains(hit.tag))
            {
                Destroy(hit, 0f);
            }
            Destroy(gameObject, 0.0f);

        }
    }
}
