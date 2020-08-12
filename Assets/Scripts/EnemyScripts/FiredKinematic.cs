using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredKinematic : MonoBehaviour
{
    public int projectileDamage = 5;
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
        GameObject hit = collision.gameObject;
        //Debug.LogFormat("kinematic bullet hit:{0}", hit.tag);
        if (hit.tag != "Projectile" || hit.tag != "Enemy") 
        {
            if (hit.tag == "Player") hit.GetComponent<CombatVariables>().DecreaseHP(projectileDamage);
            Destroy(gameObject, 0.0f);
            
        }
    }
}
