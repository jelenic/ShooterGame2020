using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRotationFix : MonoBehaviour
{
    Quaternion startRotation;
    Vector2 startPosition;
    Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
