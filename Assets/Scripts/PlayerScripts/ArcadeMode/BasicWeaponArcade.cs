using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWeaponArcade : MonoBehaviour
{
    public GameObject projectile;
    public float rateOfFire;
    public float weaponDamageModifier;
    public float destroyableNumber;

    protected virtual void init() { }

    private void Awake()
    {
        destroyableNumber = Mathf.Max(1f, destroyableNumber);
        init();
    }

    protected Transform playerTransform;


    public void setPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }


    public virtual void fire(float playerDamagerModifier = 1f, float speedModifier = 1f) { }

    public virtual void upgrade() { }

    
}
