
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipement
{
    public int baseDamage = 10;
    public string damageType;   
}
