using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredKinematic : FiredProjectile
{
    private float velocity;
    private bool deflected;

    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 5;
        lifeDuration = 5f;
        velocity = 9;

        passThrough.Add("Enemy");
        passThrough.Add("Spawner");
        passThrough.Add("Projectile");

        damageable.Add("Player");
        deflected = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (deflected)
        {
            transform.Translate(- Vector2.up * Time.deltaTime * velocity, Space.Self);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocity, Space.Self);
        }
    }

    public void Deflect()
    {
        Debug.Log("deflected");
        deflected = true;
        passThrough.Remove("Enemy");
        passThrough.Add("Player");
        damageable.Add("Enemy");
    }
}
