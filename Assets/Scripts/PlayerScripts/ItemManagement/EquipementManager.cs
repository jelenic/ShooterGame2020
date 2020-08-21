using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementManager : MonoBehaviour
{
    #region EquipementSingleton
    public static EquipementManager instance;
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

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipementSlot)).Length;
        currentlyEquiped = new Equipement[numSlots];

        inventory = Inventory.instance;
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

    public void unequip()
    {

    }
}
