using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityShieldModule : MonoBehaviour, IShield
{
    public ShieldType getShieldType() => ShieldType.ImmunityShield;

}
