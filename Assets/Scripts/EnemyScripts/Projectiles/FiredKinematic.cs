using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredKinematic : FiredProjectile
{
    private float alive;

    public override void Initialize()
    {
        base.Initialize();
        alive = 1f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if ( alive <= lifeDuration) alive += Time.deltaTime*3;
        transform.Translate(speedConstant * velocityModifier * Time.deltaTime * (alive / lifeDuration), Space.Self);
    }

    public override void Deflect()
    {
        speedConstant *= -1f;
        damageable.Add("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }
}
