using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipementScript : MonoBehaviour
{
    private Transform player;
    private Stats stats;

    private Equipement specialEquip;
    private IActiveOrCharging specialEquipScript;
    private Transform specialEquipTransform;

    public Image specialEquipCD;
    public TextMeshProUGUI specialEquipCurrentAmmoText;
    public TextMeshProUGUI specialEquipTotalAmmoText;


    private int specialEquipAmmo;

    private void speCDCallback(float filled)
    {
        specialEquipCD.fillAmount = filled;
    }

    private void speAmmoRefresh()
    {
        specialEquipCurrentAmmoText.text = specialEquipAmmo.ToString();
        Debug.Log($"mag size:{5+5}, stat mag: {stats.magazineModifier}, null: {specialEquipTotalAmmoText == null}");
        specialEquipTotalAmmoText.text = (specialEquip.magazineSize + stats.magazineModifier).ToString();
    }

    private bool speAmmoCheck()
    {
        if (specialEquipAmmo > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void onSpeActivated()
    {
        specialEquipAmmo -= 1;
        speAmmoRefresh();
    }

    private void Awake()
    {
        player = GetComponent<Transform>();
        stats = GetComponent<Stats>();
    }
    public bool Equip(Equipement eq)
    {
        //Debug.Log(eq.GetType().ToString() + " eq script equping " + eq.name);
        return SpecialWeaponEquip(eq);
    }

    private bool SpecialWeaponEquip(Equipement eq)
    {
        if (specialEquip != null)
        {
            if (eq.name.Equals(specialEquip.name))
            {
                if (specialEquipAmmo.Equals(specialEquip.magazineSize + stats.magazineModifier)) return false;
                int ammoAmount = Mathf.CeilToInt((Random.Range(0.5f, 1f) * specialEquip.magazineSize));
                specialEquipAmmo = Mathf.Min(specialEquipAmmo + ammoAmount, (specialEquip.magazineSize + stats.magazineModifier));
                speAmmoRefresh();

                return true;
            } else if (specialEquipScript.isActiveOrCharging())
            {
                return false;
            }
            specialEquipTransform.parent = null;
            Destroy(specialEquipTransform.gameObject);
        }

        specialEquipAmmo = Mathf.CeilToInt((Random.Range(0.5f, 1f) * eq.magazineSize));

        if (eq.GetType().Equals(typeof(SpecialWeapon)))
        {
            SpecialWeapon specialWeapon = eq as SpecialWeapon;
            specialEquip = specialWeapon;

            GameObject specialWeapon_obj = Resources.Load<GameObject>("Equipement/SpecialWeapons/" + specialWeapon.codeName);
            GameObject spw = Instantiate(specialWeapon_obj, player.position, player.rotation);
            specialEquipTransform = spw.transform;
            spw.transform.parent = player;
            SpecialWeaponScript spwComponent = spw.GetComponentInChildren<SpecialWeaponScript>();
            spwComponent.setParams(specialWeapon, speAmmoCheck);
            spwComponent.OnCooldownChangedCallback += speCDCallback;
            spwComponent.OnFiredCallback += onSpeActivated;

            specialEquipScript = spwComponent;
        }
        else
        {
            Module module = eq as Module;
            specialEquip = module;

            GameObject module_obj = Resources.Load<GameObject>("Equipement/Modules/" + module.codeName);
            GameObject module_instance = Instantiate(module_obj, player.position, player.rotation);
            specialEquipTransform = module_instance.transform;
            module_instance.transform.parent = player;
            ModuleScript moduleComponent = module_instance.GetComponentInChildren<ModuleScript>();
            moduleComponent.setParams(module);
            moduleComponent.OnCooldownChangedCallback += speCDCallback;
            moduleComponent.OnActivatedCallback += onSpeActivated;


            specialEquipScript = moduleComponent;
        }
        speAmmoRefresh();
        return true;
    }
}

public interface IActiveOrCharging
{
    bool isActiveOrCharging();
}
