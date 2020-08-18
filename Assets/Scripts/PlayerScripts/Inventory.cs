using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items;
    private Dictionary<string, Item> itemDB;


    private void initDB()
    {
        GameObject[] allItems = Resources.LoadAll<GameObject>("Items");

        foreach (GameObject item in allItems)
        {
            Item itemDetails = item.GetComponent<Item>();
            itemDB.Add(itemDetails.name, itemDetails);
        }
    }



    private void Awake()
    {
        items = new List<string>();
        itemDB = new Dictionary<string, Item>();
        initDB();
    }





    public void addItem(string name)
    {
        items.Add(name);
        Debug.LogFormat("item {0} was added to inventory", name);
    }
}
