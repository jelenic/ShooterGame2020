using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image module_cdbar;
    public Image special_weapon_cdbar;

    private void Awake()
    {
        Invoke("lol", 0.3f);
    }

    void lol()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ModuleScript>().OnCooldownChangedCallback += module_change;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpecialWeaponScript>().OnCooldownChangedCallback += special_weapon_change;
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ModuleScript>().OnCooldownChangedCallback -= module_change;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ModuleScript>().OnCooldownChangedCallback -= special_weapon_change;
    }

    private void module_change(float filled)
    {
        module_cdbar.fillAmount = filled;
    }
    
    private void special_weapon_change(float filled)
    {
        special_weapon_cdbar.fillAmount = filled;
    }
}
