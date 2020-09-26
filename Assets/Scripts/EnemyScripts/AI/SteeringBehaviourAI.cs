using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviourAI : MonoBehaviour
{


    Stats stats;

    Rigidbody2D rb;
    public Transform target;
    private Transform player;
    private Transform startPosition;

    private Vector3 velocity;
    private float MaxVelocity;
    private float MaxForce;


    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        MaxVelocity = 250;
        MaxForce = 50;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 desiredVelocity = target.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        //steering /= 2;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        //transform.position += velocity * Time.deltaTime;
        //transform.forward = velocity.normalized;
        rb.AddForce(velocity);

    }
}
