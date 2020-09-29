using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiredBullet : FiredProjectile
{

    private Rigidbody2D rb;
    public override void Initialize()
    {
        base.Initialize();

        passThrough.Add("Player");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("PlayerProjectile");
        passThrough.Add("Item");

        damageable.Add("Enemy");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position += transform.up * Time.deltaTime * projectileSpeed * velocityModifier);
    }


}
