using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerData : MonoBehaviour
{
    public Module module;
    public Weapon weapon1;
    public Weapon weapon2;

    private EquipementManager em;
    // Start is called before the first frame update
    void Start()
    {

        em = EquipementManager.instance;
        Invoke("testEquip", 2f);
    }

    void testEquip()
    {
        em.equip(module, EquipementSlot.Module);
        em.equip(weapon1, EquipementSlot.Weapon1);
        em.equip(weapon2, EquipementSlot.Weapon2);
    }
}
