using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float BulletForce;
    private float maxVelocity;
    private Rigidbody2D rb;
    public static bool canHit;
    public GameObject explosion;
    private Transform bullet;

    // Use this for initialization
    void Start()
    {
        BulletForce = 20;
        maxVelocity = 150;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
        rb.AddForce(transform.up * BulletForce);
        bullet = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        ClampVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("collision");


    }


    private void OnDestroy()
    {
        Instantiate(explosion, bullet.position, bullet.rotation);
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log("collision");

        /*if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }*/
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("hit an enemy dyn");
            Destroy(gameObject, 0.0f);
        }

        else if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject, 0.0f);
        }

    }

}
