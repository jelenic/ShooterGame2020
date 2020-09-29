
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equipement : ConsumableItem
{
    public int magazineSize;
    public override bool consume(CombatVariables cv)
    {
        return cv.handleEquipement(this);
    }
}


public enum EquipementSlot { Weapon1, Weapon2, SpecialWeapon, Module}