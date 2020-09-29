using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHP : MonoBehaviour, Damageable
{
    private EnemyHPShield ehp;
    private void Awake()
    {
        ehp = gameObject.GetComponentInParent<EnemyHPShield>();
    }
    public int IncreaseHP(int amount) { ehp.getDmg(amount); return (int) ehp.remainingTime; }
    public int DecreaseHP(int amount, DamageType type) { ehp.getDmg(-amount); return 1; }
}
