using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Text category;
    public GameObject itemDetails;

    public Transform itemsParent;
    InventorySlot[] slots;
    private Equipement currentCategory;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = Inventory.instance;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory.OnInventoryChangedCallback += updateUI;
    }

    private void OnDestroy()
    {
        inventory.OnInventoryChangedCallback -= updateUI;

    }

    public void loadItemCategory(EquipementSlot slot)
    {

        switch(slot)
        {
            case EquipementSlot.Weapon1:
            case EquipementSlot.Weapon2:
                currentCategory = new Weapon();
                break;
            case EquipementSlot.Module:
                currentCategory = new Module();
                break;
            default:
                currentCategory = new Equipement();
                break;
        }

        itemDetails.SetActive(false);

        category.text = slot.ToString();
        gameObject.active = true;

        updateUI();

    }

    void updateUI()
    {
        Debug.Log("updating ui");

        int filled = 0;
        foreach (Item it in inventory.items)
        {
            if (it.GetType().Equals(currentCategory.GetType()))
            {
                slots[filled++].addItem(it);
            }
        }
        for (; filled < slots.Length; filled++)
        {
                slots[filled].clearSlot();
        }

    }

}
