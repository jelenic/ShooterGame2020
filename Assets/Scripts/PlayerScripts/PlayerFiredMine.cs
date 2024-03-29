﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiredMine : FiredProjectile
{
    public CircleCollider2D mainCollider;
    public CircleCollider2D effectCollider;

    private bool activated;

    private float movingTime;
    public override void Initialize()
    {
        base.Initialize();

        movingTime = 1f;

        mainCollider.enabled = true;
        effectCollider.enabled = false;
        activated = false;
    }

    private void FixedUpdate()
    {
        if (movingTime >= 0f)
        {
            movingTime -= Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime * 3f, Space.Self);
        }
    }

    protected override void activate(GameObject hit)
    {
        if (!activated)
        {
            if (destroyable.Contains(hit.tag) && ++destroyed == destroyableNumber)
            {
                activated = true;
                mainCollider.enabled = false;
                effectCollider.enabled = true;
                Destroy(gameObject, 0.02f);
            }
        }
        else
        {
            if (damageable.Contains(hit.tag))
            {
                int target_hp = hit.GetComponent<Damageable>().DecreaseHP((int)Mathf.Round(projectileDamage * damageModifier), projectileDamageType);
                if (target_hp.Equals(0) && levelManager != null) levelManager.weaponKill(weaponName);
            }
        }
    }

}
