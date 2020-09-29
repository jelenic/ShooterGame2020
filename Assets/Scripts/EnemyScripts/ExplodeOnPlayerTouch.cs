using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnPlayerTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject, 0f);
            collision.collider.gameObject.GetComponent<Damageable>().DecreaseHP(10, DamageType.Physical);
        }
    }
}
