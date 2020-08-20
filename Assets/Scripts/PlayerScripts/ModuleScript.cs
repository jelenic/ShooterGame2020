using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleScript : MonoBehaviour
{
    //defence, movement, ofense
    private enum type{WithSprite, WithParticles, NoEffect};
    [SerializeField] protected float coooldown;


}
