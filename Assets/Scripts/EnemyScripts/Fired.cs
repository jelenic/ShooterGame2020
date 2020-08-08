using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fired : MonoBehaviour
{
    public float force;
    private float maxVelocity;
    private Rigidbody2D rb;
    public float lifeDuration;
    public GameObject explosion;
    private Transform bullet;

    // Use this for initialization
    void Start()
    {
        force = 3;
        maxVelocity = 100;
        rb = GetComponent<Rigidbody2D>();
        lifeDuration = 5f;
        Destroy(gameObject, lifeDuration);
        rb.AddForce(transform.up * force);
        bullet = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        ClampVelocity();
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
        Destroy(gameObject, 0.0f);
        Debug.LogFormat("hit:{0}", collision.gameObject.tag);
        

    }
}
