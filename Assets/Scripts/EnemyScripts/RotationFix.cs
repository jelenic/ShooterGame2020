using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour
{
    Quaternion startRotation;
    Transform transform;
    public Transform parent;
    public float translationFix = 1.7f;
    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
        startRotation = new Quaternion(0, 0, 0, 1);

        parent = transform.parent;

        transform.rotation = startRotation;
        transform.position = parent.position + Vector3.up * translationFix;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.position + Vector3.up * translationFix;
        transform.rotation = startRotation;
    }
}
