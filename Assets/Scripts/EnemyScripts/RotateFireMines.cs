using UnityEngine;

public class RotateFireMines : RotateTowardsTarget
{
    protected override void fire()
    {

        FiredMine fm = Instantiate(bullet, transform.position + transform.up, transform.rotation).GetComponent<FiredMine>();
        fm.damageModifier = stats.calculateFinalDmgModifier();
        fm.velocityModifier = stats.projectileVelocityModifier * 4f;
        fm.movingTime = distance / 5f;

        StartCoroutine(fireCooldown());

    }

    protected override float cooldownFunc()
    {
        return Random.Range(stats.rateOfFire * 0.8f, stats.rateOfFire * 1.2f);
    }
}