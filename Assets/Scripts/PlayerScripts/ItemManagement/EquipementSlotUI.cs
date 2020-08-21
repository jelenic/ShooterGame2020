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
    private Equipement eq;
    public GameObject itemDetails;
    private ItemDetailsController idc;



    private void Awake()
    {
        equipementManager = EquipementManager.instance;
        equipementManager.OnEquipementChangedCallback += equip;
        inventoryUI = InventoryPanel.GetComponent<InventoryUI>();
        idc = itemDetails.GetComponent<ItemDetailsController>();

    }
    public void equip(EquipementSlot changedSlot)
    {
        if (changedSlot == slot)
        {
            
            Debug.LogFormat("equip slot {0} {1} {2} was modified", changedSlot.ToString(), (int)slot, equipementManager.currentlyEquiped[(int)slot].name);

            if (icon != null)
            {
                eq = equipementManager.currentlyEquiped[(int)slot];
                icon.sprite = eq.icon;
                icon.enabled = true;
            }
            

        }
    }

    public void onEquipementSlotClick()
    {

        equipementManager.currentlySelected = slot;
        inventoryUI.loadItemCategory(slot);

        if (eq != null)
        {
            idc.setItemDetails(eq);
            idc.enableEquipBtn(false);
            itemDetails.SetActive(true);
        }
    }
}
