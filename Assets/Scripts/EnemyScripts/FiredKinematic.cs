using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredKinematic : MonoBehaviour
{
    private float velocity;
    public float lifeDuration;
    public GameObject explosion;
    private Transform transform;

    // Use this for initialization
    void Start()
    {
        velocity = 9;
        lifeDuration = 5f;
        Destroy(gameObject, lifeDuration);
        transform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * velocity, Space.Self);
    }


    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogFormat("kinematic bullet hit:{0}", collision.gameObject.tag);
        if (collision.gameObject.tag != "Projectile")
        {
            Destroy(gameObject, 0.0f);
            
        }
    }
}
