using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : MonoBehaviour
{
    public GameObject explosion;
    private Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
