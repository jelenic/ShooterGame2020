using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredMissile : FiredProjectile
{
    private float velocity;
    private float maxVelocity;
    private float armingDistance;
    private bool deflected;
    private float alive;
    private Transform player;
    //private Transform target;
    private float angleSpeed;
    private bool armed;

    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 5;
        projectileDamageType = DamageType.Projectile;

        lifeDuration = 10f;
        velocity = 10;
        maxVelocity = 200;
        alive = 1f;
        armingDistance = 30f;
        armed = false;

        passThrough.Add("Enemy");
        passThrough.Add("Spawner");
        passThrough.Add("Shield");
        passThrough.Add("Projectile");
        passThrough.Add("EnemyShield");


        damageable.Add("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //target = player;
        angleSpeed = 0.1f;
        deflected = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("in update");

        if (!armed && Vector2.Distance(player.position, gameObject.transform.position) <= armingDistance)
        {
            armed = true;
            angleSpeed = 6f;
            //Debug.Log("armed");
        }

        if (armed && velocity <= maxVelocity)
        {
            velocity += 35 * Time.deltaTime;
            if (angleSpeed >= 0.2)
            {
                angleSpeed -= 4 * Time.deltaTime;
            }
            //Debug.Log("increase velocity");
        }

        //consider aiming at the specific point instead of following a player

        Vector2 directionPlayer = player.position - transform.position;
        float angle = Mathf.Atan2(directionPlayer.y, directionPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, angleSpeed * Time.deltaTime);



        if (alive <= lifeDuration) alive += Time.deltaTime * 3;




        transform.Translate(Vector2.up * Time.deltaTime * velocity * velocityModifier, Space.Self);
    }

    public void Deflect()
    {
        Destroy(gameObject, 0f);

    }
}
