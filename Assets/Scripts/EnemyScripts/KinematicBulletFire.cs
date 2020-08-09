using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicBulletFire : MonoBehaviour
{

    private Rigidbody2D rb;
    public GameObject explosion;
    //private Transform bullet;
    private float speed;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
        // set speed (20 is fast, 5 is slow)
        speed = 7f;
        //bullet = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += transform.up * Time.deltaTime * speed;
        rb.MovePosition(transform.position += transform.up * Time.deltaTime * speed);
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("collision");
        if (collision.gameObject.tag == "Player")
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
