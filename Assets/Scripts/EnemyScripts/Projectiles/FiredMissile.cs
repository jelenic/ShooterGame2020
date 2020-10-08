using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredMissile : FiredProjectile
{
    private float velocity;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float armingDistance;
    private bool deflected;
    private float alive;
    private Transform player;
    [SerializeField]
    private float angleSpeed;
    private bool armed;

    public override void Initialize()
    {
        base.Initialize();
        velocity = projectileSpeed;
        alive = 1f;
        armed = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
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

        transform.Translate(speedConstant * Time.deltaTime * velocity * velocityModifier, Space.Self);
    }

    public override void Deflect()
    {
        speedConstant *= -1f;
        damageable.Add("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }
}
