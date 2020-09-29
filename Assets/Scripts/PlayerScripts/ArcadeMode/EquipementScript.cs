using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipementScript : MonoBehaviour
{
    private Transform player;
    private Stats stats;

    private SpecialWeapon specialWeapon;
    private SpecialWeaponScript specialWeaponScript;
    private Transform specialWeaponTransform;

    public Image weaponCD;
    public TextMeshProUGUI weaponCurrentAmmoText;
    public TextMeshProUGUI weaponTotalAmmoText;

    public Image specialWeaponCD;
    public TextMeshProUGUI specialWeaponCurrentAmmoText;
    public TextMeshProUGUI specialWeaponTotalAmmoText;


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
        //Debug.Log(eq.GetType().ToString() + " eq script equping " + eq.name);
        SpecialWeaponEquip(eq);
    }

    private void SpecialWeaponEquip(Equipement eq)
    {
        if (specialWeapon != null)
        {
            if (eq.name.Equals(specialWeapon.name))
            {
                int ammoAmount = (int)(Random.Range(0.5f, 1f) * (specialWeapon.magazineSize + stats.magazineModifier * 2));
                specialWeaponAmmo = Mathf.Min(specialWeaponAmmo + ammoAmount, (specialWeapon.magazineSize + stats.magazineModifier * 2));
                spwAmmoRefresh();

                return;
            } else if (specialWeaponScript.isActiveOrCharging())
            {
                Debug.LogError("rip, it is active or charging");
            }
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
        specialWeaponAmmo = (int)(Random.Range(0.5f, 1f) * (specialWeapon.magazineSize + stats.magazineModifier * 2));
        //Debug.Log("sp amo " + specialWeaponAmmo);
        spwAmmoRefresh();

        specialWeaponScript = GetComponentInChildren<SpecialWeaponScript>();
    }

    private void ModuleEquip(Equipement eq)
    {

    }
}
