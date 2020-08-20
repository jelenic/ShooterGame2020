using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    bool triggered = false;
    public Item item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            pickUp();
            
        }
    }

    void pickUp()
    {
        Debug.LogFormat("player just collected {0}", item.name);
        if (Inventory.instance.add(item)) Destroy(gameObject);
    }
}
