using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredKinematic : FiredProjectile
{
    private float velocity;

    public override void Initialize()
    {
        base.Initialize();
        projectileDamage = 5;
        lifeDuration = 5f;
        velocity = 9;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * velocity, Space.Self);
    }
}
