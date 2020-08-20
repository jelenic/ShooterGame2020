using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Text category;

    public Transform itemsParent;
    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void loadItemCategory(Item item)
    {
        Debug.LogFormat("this button is for category {0}", item.GetType().Name);

        category.text = item.GetType().Name;
        gameObject.active = true;

        updateUI(item);

    }

    void updateUI(Item item)
    {
        Debug.Log("updating ui");

        int filled = 0;
        foreach (Item it in inventory.items)
        {
            if (it.GetType().Equals(item.GetType()))
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
