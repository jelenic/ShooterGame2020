using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipementScript : MonoBehaviour
{
    private Transform player;
    private Stats stats;

    private Weapon weapon;
    private Transform weaponTransform;

    private SpecialWeapon specialWeapon;
    private Transform specialWeaponTransform;

    public Image weaponCD;
    public TextMeshProUGUI weaponCurrentAmmoText;
    public TextMeshProUGUI weaponTotalAmmoText;

    public Image specialWeaponCD;
    public TextMeshProUGUI specialWeaponCurrentAmmoText;
    public TextMeshProUGUI specialWeaponTotalAmmoText;

    private int weaponAmmo;
    private int specialWeaponAmmo;

    private void wCDCallback(float filled)
    {
        weaponCD.fillAmount = filled;
    }
    private void spwCDCallback(float filled)
    {
        specialWeaponCD.fillAmount = filled;
    }

    private void spwAmmoRefresh()
    {
        specialWeaponCurrentAmmoText.text = specialWeaponAmmo.ToString();
        specialWeaponTotalAmmoText.text = (specialWeapon.magazineSize + stats.magazineModifier*2).ToString();
    }

    private bool spwAmmoCheck()
    {
        if (specialWeaponAmmo > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void onSpwFired()
    {
        specialWeaponAmmo -= 1;
        spwAmmoRefresh();
    }



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
                WeaponEquip(eq);
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
        SpecialWeaponScript spwComponent = spw.GetComponentInChildren<SpecialWeaponScript>();
        spwComponent.setParams(specialWeapon, spwAmmoCheck);
        spwComponent.OnCooldownChangedCallback += spwCDCallback;
        spwComponent.OnFiredCallback += onSpwFired;
        specialWeaponAmmo = (int)(Random.Range(0.5f, 1f) * (specialWeapon.magazineSize + stats.magazineModifier*2));
        //Debug.Log("sp amo " + specialWeaponAmmo);
        spwAmmoRefresh();
    }

    private void ModuleEquip(Equipement eq)
    {

    }
}
