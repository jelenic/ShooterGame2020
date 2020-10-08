using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiredBullet : FiredProjectile
{
    private Vector2 speedConstant;
    private void Start()
    {
        speedConstant = Vector2.up * projectileSpeed;
    }
    private void FixedUpdate()
    {
        transform.Translate(speedConstant * velocityModifier * Time.deltaTime, Space.Self);
    }


}
