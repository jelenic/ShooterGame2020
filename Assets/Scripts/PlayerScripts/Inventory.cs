using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public void addItem(string id)
    {
        Debug.LogFormat("item {0} was added to inventory", id);
    }
}
