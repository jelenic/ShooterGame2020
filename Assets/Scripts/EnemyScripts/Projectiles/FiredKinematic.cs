using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredKinematic : FiredProjectile
{
    private float velocity;
    private bool deflected;
    private float alive;

    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 5;
        projectileDamageType = DamageType.Projectile;

        lifeDuration = 5f;
        velocity = 20;
        alive = 1f;

        passThrough.Add("Enemy");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("Projectile");
        passThrough.Add("EnemyShield");


        damageable.Add("Player");
        deflected = false;
    }


    // Update is called once per frame
    void Update()
    {
        if ( alive <= lifeDuration) alive += Time.deltaTime*3;
        if (deflected)
        {
            transform.Translate(- Vector2.up * Time.deltaTime * velocity * velocityModifier * (alive / lifeDuration), Space.Self);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocity * velocityModifier * (alive / lifeDuration), Space.Self);
        }
    }

    public void Deflect()
    {
        deflected = true;
        passThrough.Remove("Enemy");
        passThrough.Add("Player");
        damageable.Add("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Projectile");

    }
}
