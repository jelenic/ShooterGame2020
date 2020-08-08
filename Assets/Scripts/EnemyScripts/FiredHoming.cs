using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredHoming : MonoBehaviour
{
    
    public float lifeDuration;
    public GameObject explosion;
    public Transform transform;

    // Use this for initialization
    void Start()
    {
        
        lifeDuration = 5f;
        Destroy(gameObject, lifeDuration);
        transform = GetComponent<Transform>();

    }

   

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            Destroy(gameObject, 0.0f);
            Debug.LogFormat("homing bullet hit:{0}", collision.gameObject.tag);
        }



    }
}
