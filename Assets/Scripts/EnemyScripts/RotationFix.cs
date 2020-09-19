using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour
{
    Quaternion startRotation;
    Transform transform;
    public float translationFix = 1.7f;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gameObject.transform.parent.transform.position + Vector3.up * translationFix;
        transform.rotation = startRotation;
    }
}
