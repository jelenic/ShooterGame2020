using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnPlayerTouch : MonoBehaviour
{
    private Rigidbody2D rb;
    private Stats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.collider.gameObject.GetComponent<Damageable>().DecreaseHP((int) (rb.velocity.magnitude*stats.damageModifier), DamageType.Projectile);
        } else if (collision.collider.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
