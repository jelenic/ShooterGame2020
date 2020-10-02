using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIRotate : BossAIArcade
{
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, Time.deltaTime * Random.Range(10f, 20f));
    }
}
