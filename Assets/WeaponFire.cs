using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject prefab;
    private Stats stats;

    private void Start()
    {
        stats = gameObject.GetComponentInParent<Stats>();
    }
    public void Shoot()
    {
        Instantiate(prefab, transform.position, transform.rotation).GetComponent<PlayerFiredBullet>().damageModifier = stats.calculateFinalDmgModifier();
    }
}
