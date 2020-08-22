using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsController : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemValue;
    public TextMeshProUGUI itemLevel;
    public TextMeshProUGUI itemStats;
    public TextMeshProUGUI itemDescription;

    public GameObject equipBtn;

    private EquipementManager equipementManager;
    private Equipement eq;

    private void Awake()
    {
        equipementManager = EquipementManager.instance;
    }
    // Start is called before the first frame update

    public void setItemDetails(Item item)
    {

        itemName.SetText(item.name);
        itemValue.SetText(item.value.ToString());
        itemLevel.SetText(item.level.ToString());
        itemDescription.SetText(item.description);

        string stats;
        switch(item.GetType().Name)
        {
            case "Weapon":
                eq = (Equipement)item;
                Weapon weapon = (Weapon)item;
                stats = string.Format("Damage modifier: {0}\nBullet type : {1}", weapon.weaponDamageModifier, weapon.bulletType);
                break;
            case "Module":
                eq = (Equipement)item;
                Module module = (Module)item;
                stats = string.Format("Cooldown : {0}", module.cooldown);
                break;
            default:
                stats = "";
                break;
        }

        itemStats.SetText(stats);
    }

    public void enableEquipBtn(bool enabled)
    {
        equipBtn.SetActive(enabled);
    }
    public void equip()
    {
        equipementManager.equip(eq);
        enableEquipBtn(false);
    }
}
