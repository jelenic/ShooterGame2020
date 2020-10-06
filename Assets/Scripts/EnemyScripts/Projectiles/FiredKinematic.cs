using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredKinematic : FiredProjectile
{
    private float velocity;
    private bool deflected;
    private float alive;
    private Vector2 projectileSpeed;

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

        projectileSpeed = Vector2.up * velocity;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if ( alive <= lifeDuration) alive += Time.deltaTime*3;
        if (deflected)
        {
            transform.Translate(-projectileSpeed * velocityModifier * Time.deltaTime * (alive / lifeDuration), Space.Self);
        }
        else
        {
            transform.Translate(projectileSpeed * velocityModifier * Time.deltaTime * (alive / lifeDuration), Space.Self);
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
