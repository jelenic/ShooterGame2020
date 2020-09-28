using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    private Vector2 pos;
    private CircleCollider2D collider;

    private List<Rigidbody2D> currentlyInside = new List<Rigidbody2D>();
    //private Rigidbody2D[] currentlyInside = new Rigidbody2D[100];
    //private int inCounter;

    public float force;
    public float waitDuration;

    private void Awake()
    {
        pos = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine(gravitate());

    }

    IEnumerator gravitate()
    {
        while (true)
        {
            foreach (Rigidbody2D r in currentlyInside)
            {
                r.AddForce((pos - r.position) * force);
            }

            yield return new WaitForSeconds(waitDuration);
            pos = new Vector2(transform.position.x, transform.position.y);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //currentlyInside[inCounter++] = collision.attachedRigidbody;
        currentlyInside.Add(collision.attachedRigidbody);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //currentlyInside[inCounter++] = collision.attachedRigidbody;
        currentlyInside.Remove(collision.attachedRigidbody);
    }


}