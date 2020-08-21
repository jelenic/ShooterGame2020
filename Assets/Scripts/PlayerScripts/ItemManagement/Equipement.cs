
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : Item
{
    public EquipementSlot equipSlot;
}


public enum EquipementSlot { Weapon1, Weapon2, SpecialWeapon, Module}