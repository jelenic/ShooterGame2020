using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorModule : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && collision.gameObject.GetComponent<FiredProjectile>().GetType().Equals(typeof(FiredKinematic)))
        {
            collision.gameObject.GetComponent<FiredKinematic>().Deflect();
        }
    }
}
