using UnityEngine;


[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item") ]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public int value;
    public int level;
    public Sprite icon = null;
    public string description;
    public Color color = Color.white;


    public void removeFromInventory()
    {
        Inventory.instance.remove(this);
    }
}
