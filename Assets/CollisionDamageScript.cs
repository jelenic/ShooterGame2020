using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageScript : MonoBehaviour
{
    private Damageable damageable;
    private Rigidbody2D rb;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        int dmg = (int) (rb.mass * rb.velocity.magnitude * 2f);
        if (dmg >= 10)
        {
            damageable.DecreaseHP(dmg, DamageType.Physical);
            Damageable damageable2 = collision.collider.GetComponent<Damageable>();
            if (damageable2 != null) damageable2.DecreaseHP(dmg, DamageType.Physical);
        }

    }
}
