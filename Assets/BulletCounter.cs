using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCounter : MonoBehaviour
{
    public static BulletCounter instance;
    public int count = 0;

    private void Awake()
    {
        instance = this;
    }

}
