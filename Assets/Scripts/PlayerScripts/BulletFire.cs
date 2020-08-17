using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public int bulletDamage = 3;
    public float damageModifier;
    private float speed;

    public float BulletForce;
    private float maxVelocity;
    private Rigidbody2D rb;
    public GameObject explosion;
    public static bool canHit;
    private Transform bullet;

    // Use this for initialization
    void Start()
    {
        BulletForce = 4;
        speed = 40;
        maxVelocity = 100;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
        //rb.AddForce(transform.up * BulletForce);
        bullet = GetComponent<Transform>();

    }

    // Update is called once per frame
    /*void Update()
    {
        ClampVelocity();
    }*/

    // use if want to have it kinematic
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position += transform.up * Time.deltaTime * speed);
    }



    private void OnDestroy()
    {
        Instantiate(explosion, bullet.position, bullet.rotation);
    }

    /*private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
    }*/



    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("collision");

        /*if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }*/
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("hit an enemy dyn");
            collision.gameObject.GetComponent<CombatVariables>().DecreaseHP((int)Math.Round(bulletDamage * damageModifier), "projectile");
            Destroy(gameObject, 0.0f);
        }

        else if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject, 0.0f);
        }

    }

}
