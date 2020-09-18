using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredZigZag : FiredProjectile
{
    private float velocity;
    private bool deflected;
    private int zig;
    private bool zag;
    public int angle;

    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 5;
        projectileDamageType = DamageType.Projectile;

        lifeDuration = 8f;
        velocity = 9;

        passThrough.Add("Enemy");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("Projectile");
        passThrough.Add("EnemyShield");


        damageable.Add("Player");
        deflected = false;
        zag = true;
    }

    private void zigZag()
    {
        if (zag)
        {
            if (zig.Equals(0)) transform.Rotate(0, 0, angle / 2);
            zig += 1;
            if (zig.Equals(5))
            {
                zag = false;
                zig = 0;
            }
        } else
        {
            if (zig.Equals(0)) transform.Rotate(0, 0, -angle);
            if (zig.Equals(10)) transform.Rotate(0, 0, angle);
            zig = (zig + 1) % 20;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        zigZag();

        if (deflected)
        {
            transform.Translate(-Vector2.up * Time.deltaTime * velocity * velocityModifier, Space.Self);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocity * velocityModifier, Space.Self);
        }
    }

    public void Deflect()
    {
        deflected = true;
        passThrough.Remove("Enemy");
        passThrough.Add("Player");
        damageable.Add("Enemy");
    }
}
