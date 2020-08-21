using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Item item;
    public GameObject itemDetails;
    private ItemDetailsController idc;
    // Start is called before the first frame update

    private void Awake()
    {
        idc = itemDetails.GetComponent<ItemDetailsController>();
    }

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


    public void setItemDetails()
    {
        if (item != null) 
        {
            idc.setItemDetails(item);
            idc.enableEquipBtn(true);
            itemDetails.SetActive(true);
        }
    }
}
