using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementScript : MonoBehaviour
{
    private Transform player;
    private Stats stats;

    private Weapon weapon;
    private Transform weaponTransform;

    private SpecialWeapon specialWeapon;
    private Transform specialWeaponTransform;

    private void Awake()
    {
        player = GetComponent<Transform>();
        stats = GetComponent<Stats>();
    }
    public void Equip(Equipement eq)
    {
        Debug.Log(eq.GetType().ToString() + " eq script equping " + eq.name);

        switch(eq.GetType().ToString())
        {
            case "Weapon":

                break;
            case "SpecialWeapon":
                SpecialWeaponEquip(eq);
                break;
        }





    }

    private void WeaponEquip(Equipement eq)
    {
        if (weapon != null)
        {

        }
        weapon = eq as Weapon;
    }
    private void SpecialWeaponEquip(Equipement eq)
    {
        if (specialWeapon != null)
        {
            specialWeaponTransform.parent = null;
            Destroy(specialWeaponTransform.gameObject);
        }

        specialWeapon = eq as SpecialWeapon;

        GameObject specialWeapon_obj = Resources.Load<GameObject>("Equipement/SpecialWeapons/" + specialWeapon.codeName);
        GameObject spw = Instantiate(specialWeapon_obj, player.position, player.rotation);
        specialWeaponTransform = spw.transform;
        spw.transform.parent = player;
        spw.GetComponentInChildren<SpecialWeaponScript>().setParams(specialWeapon);
    }

    private void ModuleEquip(Equipement eq)
    {

    }
}
