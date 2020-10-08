using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiredZigZag : FiredProjectile
{
    private bool deflected;
    public int zig;
    public bool zag;

    [Range(10, 90)]
    public int angle;

    [Range(2, 20)]
    public int frequency;

    public override void Initialize()
    {
        base.Initialize();
        deflected = false;
        zag = true;
    }

    private void zigZag()
    {
        if (zag)
        {
            if (zig++.Equals(0)) transform.Rotate(0, 0, angle / 2);
            if (zig.Equals(frequency/2))
            {
                zag = false;
                zig = 0;
            }
        } else
        {
            if (zig.Equals(0)) transform.Rotate(0, 0, -angle);
            if (zig.Equals(frequency)) transform.Rotate(0, 0, angle);
            zig = (zig + 1) % (frequency*2);
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zigZag();

        transform.Translate(speedConstant * Time.deltaTime * velocityModifier, Space.Self);
    }

    public override void Deflect()
    {
        speedConstant *= -1f;
        damageable.Add("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }

}
