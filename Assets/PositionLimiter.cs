using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLimiter : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10,10), Mathf.Clamp(transform.position.y, -10, 10));
    }
}
