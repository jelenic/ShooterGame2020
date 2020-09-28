using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredMine : FiredProjectile
{
    public float movingTime;
    public override void Initialize()
    {
        base.Initialize();

        passThrough.Add("Enemy");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("Projectile");
        passThrough.Add("EnemyShield");

        damageable.Add("Player");
    }

    private void FixedUpdate()
    {
        if (movingTime >= 0f)
        {
            movingTime -= Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime * velocityModifier, Space.Self);
        }
    }
}
