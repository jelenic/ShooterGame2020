using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New special weapon", menuName = "Inventory/SpecialWeapon")]
public class SpecialWeapon : Equipement
{
    public int damageBase;
    public float cooldown;
    public string damageType;
    public string specialEffect;
    public float chargeSpeed;
    public float minCharge;
    public float maxCharge;
}
