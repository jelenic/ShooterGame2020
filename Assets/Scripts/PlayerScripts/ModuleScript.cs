using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleScript : MonoBehaviour
{
    //defence, movement, ofense
    private enum type{WithSprite, WithParticles, NoEffect};
    [SerializeField] protected float coooldown;
    [SerializeField] protected float duration;

    public void setParams(float cooldown, float duration)
    {
        if (cooldown >= 0) coooldown = cooldown;
        if (duration >= 0) this.duration = duration;
    }


}
