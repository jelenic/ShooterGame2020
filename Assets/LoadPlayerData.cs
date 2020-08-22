using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerData : MonoBehaviour
{
    public Module module;
    public Weapon weapon1;
    public Weapon weapon2;

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
        em.equip(module, EquipementSlot.Module);
        em.equip(weapon1, EquipementSlot.Weapon1);
        em.equip(weapon2, EquipementSlot.Weapon2);


        foreach(Item item in addToInventory)
        {
            inv.add(item);
        }
    }
}
