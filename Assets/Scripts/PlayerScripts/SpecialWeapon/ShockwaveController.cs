using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
    private Transform transform;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private Stats parentStats;
    public bool effectActive;


    public delegate void EffectDamage(GameObject affected);
    private EffectDamage effectDamage;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void setParams(EffectDamage method)
    {
        effectDamage = method;
        circleCollider2D.enabled = true;
        effectActive = true;
    }

    public void chargeRefresh(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }

    public void wave(float scale)
    {
        transform.localScale *= scale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            effectDamage(collision.gameObject);
        } 
        else if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
