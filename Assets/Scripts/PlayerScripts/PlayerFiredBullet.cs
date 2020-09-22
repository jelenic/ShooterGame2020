using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiredBullet : FiredProjectile
{

    private Rigidbody2D rb;
    private float speed;
    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 3;
        lifeDuration = 5f;

        passThrough.Add("Player");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("PlayerProjectile");
        passThrough.Add("Item");

        damageable.Add("Enemy");

        destroyable.Add("Projectile");
    }

    void Start()
    {
        speed = 40;
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position += transform.up * Time.deltaTime * speed * velocityModifier);
        //transform.position += transform.up * Time.deltaTime * speed * velocityModifier;
    }


}
