using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    Transform transform;

    void Awake()
    {
        transform = gameObject.GetComponent<Transform>();
    }

    public void refresh(float scale, Vector3 position)
    {
        transform.localScale = Vector3.one * scale;
        transform.position = position;

    }
}
