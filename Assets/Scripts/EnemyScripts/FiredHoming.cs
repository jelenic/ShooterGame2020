using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredHoming : MonoBehaviour
{
    public int projectileDamage = 3;
    public float lifeDuration;
    public GameObject explosion;
    public Transform transform;

    // Use this for initialization
    void Start()
    {
        
        lifeDuration = 20f;
        Destroy(gameObject, lifeDuration);
        transform = GetComponent<Transform>();

    }

   

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    

    void OnTriggerEnter2D(Collider2D collision)
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

