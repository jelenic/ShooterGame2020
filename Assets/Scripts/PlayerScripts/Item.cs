using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string id;
    public string name;
    public int value;
    public int rarity;
    public string type { get; protected set; }
    public string description;

    protected GameObject gameManager;

    public virtual void Initialize()
    {

    }

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        Initialize();
    }

    public void addToInventory()
    {
        gameManager.GetComponent<Inventory>().addItem(name);
    }



}
