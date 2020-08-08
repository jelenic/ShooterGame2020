using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotateTowardsTarget : MonoBehaviour
{
    public float speed;
    public float range;
    public Quaternion defaultRotation;
    public Transform target;
    public GameObject bullet;

    // Use this for initialization
    void Start () {
        speed = 10f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        range = 13f;
        defaultRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, target.position) <= range)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            Instantiate(bullet, transform.position, transform.rotation);

        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, speed * Time.deltaTime);
        }
        
	}
}
