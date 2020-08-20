using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnInventoryChanged();
    public OnInventoryChanged OnInventoryChangedCallback;

    #region InventorySingleton
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of inventory found!");
        }
        instance = this;

    }
    #endregion

    public List<Item> items = new List<Item>();
    // Start is called before the first frame update


    public bool add(Item item)
    {
        items.Add(item);

        Debug.LogFormat("item of class {0} was added", item.GetType().Name);

        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();

        return true;
    }


    public bool remove(Item item)
    {
        items.Remove(item);

        if (OnInventoryChangedCallback != null) OnInventoryChangedCallback.Invoke();

        return true;
    }
}