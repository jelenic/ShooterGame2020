using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New module", menuName = "Inventory/Module")]
public class Module : Equipement
{
    public float cooldown;
    public float duration;
}
