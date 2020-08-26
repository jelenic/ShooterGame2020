using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    private EquipementManager equipementManager;

    public Weapon weapon1;
    public Weapon weapon2;
    public Module module;

    private void Awake()
    {
        equipementManager = EquipementManager.instance;

        equipPlayer();


    }

    public void equipPlayer()
    {
        weapon1 = equipementManager.currentlyEquiped[(int)EquipementSlot.Weapon1] as Weapon;
        weapon2 = equipementManager.currentlyEquiped[(int)EquipementSlot.Weapon2] as Weapon;
        module = equipementManager.currentlyEquiped[(int)EquipementSlot.Module] as Module;

        Debug.LogFormat("player equipement: {0} as weapon1, {1} as weapon2, {2} as weapon3", weapon1.name, weapon2.name, module.name);

        GameObject weapon1_obj = Resources.Load<GameObject>("Equipement/Weapons/" + weapon1.codeName);
        GameObject weapon2_obj = Resources.Load<GameObject>("Equipement/Weapons/" + weapon2.codeName);
        GameObject module_obj = Resources.Load<GameObject>("Equipement/Modules/" + module.codeName);


        Transform player = GetComponent<Transform>();

        GameObject w1 = Instantiate(weapon1_obj, player.position, Quaternion.identity);
        GameObject w2 = Instantiate(weapon2_obj, player.position, Quaternion.identity);
        GameObject mod = Instantiate(module_obj, player.position, Quaternion.identity);

        w1.transform.parent = player;
        w1.GetComponent<WeaponFire>().setDmgModifier(weapon1.weaponDamageModifier);
        w2.transform.parent = player;
        w2.GetComponent<WeaponFire>().setDmgModifier(weapon2.weaponDamageModifier);
        mod.transform.parent = player;
        mod.GetComponent<ModuleScript>().setParams(module.cooldown, module.duration);

        ShipFire sf = gameObject.GetComponent<ShipFire>();

        sf.setWeapons(w1, weapon1, w2, weapon2);
    }
}
