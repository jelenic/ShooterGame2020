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


        SaveData saveData = SaveManager.instance.loadFromFile();

        Equipement[] eq = saveData.equipement.Select(e => Inventory.instance.itemToId[e] as Equipement).ToArray();

        em.equip(eq[0], EquipementSlot.Weapon1);
        em.equip(eq[1], EquipementSlot.Weapon2);
        em.equip(eq[2], EquipementSlot.SpecialWeapon);
        em.equip(eq[3], EquipementSlot.Module);




    }
}
