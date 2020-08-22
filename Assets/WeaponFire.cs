using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject prefab;
    private Stats stats;
    private float weaponDamageModifier = 1f;

    public void setDmgModifier(float modifier)
    {
        if (modifier >= 0f) weaponDamageModifier = modifier;
    }


    private void Start()
    {
        stats = gameObject.GetComponentInParent<Stats>();
    }
    public void Shoot()
    {
        Instantiate(prefab, transform.position, transform.rotation).GetComponent<PlayerFiredBullet>().damageModifier = stats.calculateFinalDmgModifier() * weaponDamageModifier;
    }
}
