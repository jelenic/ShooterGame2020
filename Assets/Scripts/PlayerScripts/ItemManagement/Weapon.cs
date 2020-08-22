
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipement
{
    public float weaponDamageModifier = 1f;
    public float rateOfFireModifier;
    public string bulletType;
}
