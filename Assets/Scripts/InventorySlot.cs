using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Item item;
    // Start is called before the first frame update
    

    public void addItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void clearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
