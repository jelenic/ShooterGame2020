using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredMine : FiredProjectile
{
    public float movingTime;

    private void FixedUpdate()
    {
        if (movingTime >= 0f)
        {
            movingTime -= Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime * velocityModifier, Space.Self);
        }
    }
}
