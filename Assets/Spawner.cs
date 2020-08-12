using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public int enemyNumber;
    private int spawnedNumber = 0;
    private Transform transform;
    public bool activated = false;
    public float cooldownTime;
    private float remainingTime = 0.0f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated && spawnedNumber < enemyNumber)
        {
            activated = true;
            //Debug.Log("player entered trigger, spawning enemies");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && activated)
        {
            activated = false;
            //Debug.Log("player exited trigger, stopping spawning enemies");
        }
    }

    private void Start()
    {
        transform = GetComponent<Transform>();
        enemyNumber = 10;
        cooldownTime = 5.0f;
    }

    private void Update()
    {

        remainingTime = Math.Max(0.0f, remainingTime - Time.deltaTime);
        if (activated && remainingTime.Equals(0.0f))
        {
            Instantiate(enemy, transform.position, transform.rotation);
            spawnedNumber += 1;
            remainingTime = cooldownTime;
        }
    }
}
