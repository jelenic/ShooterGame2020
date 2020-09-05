using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EquipementManager : MonoBehaviour
{
    #region EquipementSingleton
    public static EquipementManager instance;

    string dataPath = "/eq.data";
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of equipement manager found!");
        }
        instance = this;

    }
    #endregion

    public Equipement[] currentlyEquiped;
    private Inventory inventory;
    public EquipementSlot currentlySelected;

    public delegate void OnEquipementChanged(EquipementSlot slot);
    public OnEquipementChanged OnEquipementChangedCallback;

    


    private void saveEquipement(EquipementSlot slot)
    {
        Debug.LogFormat("saving {0} equipement", currentlyEquiped.Length);

        int[] equipementForSaving = new int[currentlyEquiped.Length];
        for (int i = 0; i < currentlyEquiped.Length; i++)
        {
            equipementForSaving[i] = inventory.itemToId.IndexOf(currentlyEquiped[i]);
        }

        SaveManager.instance.saveToFile(new SaveData(equipementForSaving), dataPath);
    }

    public void loadEquipement()
    {
        SaveData saveData = SaveManager.instance.loadFromFile(dataPath);

        Debug.Log("loadedddd");
        foreach (int i in saveData.equipement) Debug.Log(i);

        Equipement[] eq = saveData.equipement.Select(e => Inventory.instance.itemToId[e] as Equipement).ToArray();

        equip(eq[0], EquipementSlot.Weapon1);
        equip(eq[1], EquipementSlot.Weapon2);
        equip(eq[2], EquipementSlot.SpecialWeapon);
        equip(eq[3], EquipementSlot.Module);
    }

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipementSlot)).Length;
        currentlyEquiped = new Equipement[numSlots];

        inventory = Inventory.instance;

        OnEquipementChangedCallback += saveEquipement;
    }

    public void equip(Equipement newEquip)
    {
        Equipement oldEquip = currentlyEquiped[(int)currentlySelected];
        if (oldEquip != null)
        {
            inventory.add(oldEquip);
        }
        currentlyEquiped[(int)currentlySelected] = newEquip;
        inventory.remove(newEquip);

        if (OnEquipementChangedCallback != null) OnEquipementChangedCallback.Invoke(currentlySelected);
    }


    public void equip(Equipement newEquip, EquipementSlot slot)
    {
        currentlySelected = slot;
        equip(newEquip);
    }

 
    public void unequip()
    {

    }
}
