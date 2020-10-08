using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorModule : MonoBehaviour, IShield
{
    public ShieldType getShieldType() => ShieldType.DeflectorShield;

}
