using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextAnimation : MonoBehaviour
{
    Transform transform;
    public float speed;
    public float duration;
    public float scaleFactor;
    // Start is called before the first frame update
    void Awake()
    {
        speed = 0.07f;
        duration = 0.75f;
        scaleFactor = 0.99f;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        duration -= Time.deltaTime;
        if (duration <= 0) Destroy(gameObject);

        transform.Translate((Vector3.up*0.8f + Vector3.right*0.2f)*speed, Space.Self);
        transform.localScale *= scaleFactor;

        
    }
}
