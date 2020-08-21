using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipementSlotUI : MonoBehaviour
{
    public EquipementSlot slot;
    public Image icon;
    public Item equiped;
    private EquipementManager equipementManager;
    public GameObject InventoryPanel;
    private InventoryUI inventoryUI;


    private void Start()
    {
        equipementManager = EquipementManager.instance;
        equipementManager.OnEquipementChangedCallback += equip;
        inventoryUI = InventoryPanel.GetComponent<InventoryUI>();


    }
    public void equip(EquipementSlot changedSlot)
    {
        if (changedSlot == slot)
        {
            Debug.LogFormat("equip slot {0} {1} {2} was modified", changedSlot.ToString(), (int)slot, equipementManager.currentlyEquiped[(int)slot].name);
            equiped = equipementManager.currentlyEquiped[(int)slot];
            icon.sprite = equiped.icon;
            icon.enabled = true;

        }
    }

    public void onEquipementSlotClick()
    {

        equipementManager.currentlySelected = slot;
        inventoryUI.loadItemCategory(slot);
    }
}
