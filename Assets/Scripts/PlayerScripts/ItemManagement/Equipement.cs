
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equipement : ConsumableItem
{
    public override void consume(CombatVariables cv)
    {
        cv.handleEquipement(this);
    }
}


public enum EquipementSlot { Weapon1, Weapon2, SpecialWeapon, Module}