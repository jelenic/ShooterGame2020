using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LoadPlayerData : MonoBehaviour
{
    

    public List<Item> addToInventory = new List<Item>();

    private EquipementManager em;
    private Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        inv = Inventory.instance;
        em = EquipementManager.instance;
        Invoke("init", 1f);
    }

    void init()
    {
        foreach(Item item in addToInventory)
        {
            inv.add(item);
        }

        em.loadEquipement();

        Levels.instance.loadScores();

    }

}
