using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInvisible : MonoBehaviour
{

    private float velocity;
    public float lifeDuration;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 100;
        lifeDuration = 0.3f;
        Destroy(gameObject, lifeDuration);
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * velocity, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        //Debug.Log("Trigger");
        //Debug.Log(hit.tag);
        //Debug.LogFormat("kinematic bullet hit:{0}", hit.tag);
        if (hit.tag == "Projectile")
        {
            Destroy(hit);
        }
        else if (hit.tag == "Enemy" || hit.tag == "Terrain")
        {
            //Debug.Log("destroying");
            Destroy(gameObject, 0f);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
        GameObject hit = collision.gameObject;
        //Debug.Log(hit.tag);
        //Debug.LogFormat("kinematic bullet hit:{0}", hit.tag);
        if (hit.tag == "Projectile")
        {
            Destroy(hit);
        }
        else if (hit.tag == "Enemy" || hit.tag == "Terrain")
        {
            //Debug.Log("destroying");
            Destroy(gameObject, 0f);
        }
        else if (collision.gameObject.tag == "PlayerProjectile")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
