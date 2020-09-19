using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image module_cdbar;
    public Image special_weapon_cdbar;

    private GameObject player;
    private ModuleScript ms;
    private SpecialWeaponScript sws;

    private void Awake()
    {
        StartCoroutine(addCallbacks());
    }

    private IEnumerator addCallbacks()
    {
        yield return new WaitForSeconds(0.3f);

        player = GameObject.FindGameObjectWithTag("Player");
        ms = player.GetComponentInChildren<ModuleScript>();
        sws = player.GetComponentInChildren<SpecialWeaponScript>();

        ms.OnCooldownChangedCallback += module_change;
        sws.OnCooldownChangedCallback += special_weapon_change;
    }

    private void OnDestroy()
    {
        ms.OnCooldownChangedCallback -= module_change;
        sws.OnCooldownChangedCallback -= special_weapon_change;
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
