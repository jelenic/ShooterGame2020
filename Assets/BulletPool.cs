using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    public Queue<GameObject> pool = new Queue<GameObject>();
    public int growSize;
    public GameObject bullet;
    public bool enabled;


    private void Awake()
    {
        instance = this;
        if (enabled) growPool();
    }

    private void growPool()
    {
        //Debug.Log("growing1 " + pool.Count);
        for (int i = 0; i < growSize; i++)
        {
            var obj = Instantiate(bullet);
            addToPool(obj, true);
        }
        Debug.Log("growing2 " + pool.Count);
    }

    public void addToPool(GameObject obj, bool fromGrow = false)
    {

        //Debug.Log(fromGrow + " adding1 " + pool.Count);
        obj.SetActive(false);
        pool.Enqueue(obj);
        //Debug.Log(fromGrow + " adding2 " + pool.Count);
    }

    public GameObject getFromPool()
    {

        //Debug.Log("getting1 " + pool.Count);
        if (pool.Count == 0)
        {
            growPool();
        }

        var obj = pool.Dequeue();
        obj.SetActive(true);
        //Debug.Log("getting2 " + pool.Count);
        return obj;
    }
}
