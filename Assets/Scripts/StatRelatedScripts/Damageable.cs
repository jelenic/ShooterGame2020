using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    int IncreaseHP(int amount);
    int DecreaseHP(int amount, DamageType type);
}
