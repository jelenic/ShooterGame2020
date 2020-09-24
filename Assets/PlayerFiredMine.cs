using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiredMine : FiredProjectile
{
    private float movingTime;
    public override void Initialize()
    {
        base.Initialize();

        movingTime = 1f;

        passThrough.Add("Player");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("PlayerProjectile");
        passThrough.Add("Item");

        damageable.Add("Enemy");
    }

    private void FixedUpdate()
    {
        if (movingTime >= 0f)
        {
            movingTime -= Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime * 3f, Space.Self);
        }
    }

}
