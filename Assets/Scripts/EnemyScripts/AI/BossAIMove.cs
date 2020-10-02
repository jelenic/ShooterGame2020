using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMove : BossAIArcade
{
    private void FixedUpdate()
    {
        transform.Translate(3f*Random.insideUnitCircle*Time.deltaTime, Space.Self);
    }
}
