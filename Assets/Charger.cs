using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    Transform transform;
    public delegate void onCharged(Vector3 position);
    public onCharged onChargedCallback;

    void Awake()
    {
        transform = gameObject.GetComponent<Transform>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 scale = transform.localScale;
        scale *= 1.008f;
        transform.localScale = scale;
    }

    private void OnDestroy()
    {
        if (onChargedCallback != null) onChargedCallback.Invoke(transform.position);
    }
}
