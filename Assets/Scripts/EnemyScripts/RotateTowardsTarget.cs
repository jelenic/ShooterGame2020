using System.Collections;
using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    protected Stats stats;
    public Quaternion defaultRotation;
    public Transform target;
    public GameObject bullet;
    public bool fireWait;
    protected float distance;
    public bool parentStats;

    // Use this for initialization
    protected virtual void Initialize()
    {
        stats = parentStats ? GetComponentInParent<Stats>() : GetComponent<Stats>();
    }
    protected virtual void fire()
    {
        FiredProjectile fp = Instantiate(bullet, transform.position + transform.up, transform.rotation).GetComponent<FiredProjectile>();
        fp.damageModifier = stats.calculateFinalDmgModifier();

        fp.velocityModifier = stats.projectileVelocityModifier;

        StartCoroutine(fireCooldown());

    }

    void Start()
    {
        Initialize();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        defaultRotation = transform.rotation;

    }

    protected IEnumerator fireCooldown()
    {
        fireWait = true;
        yield return new WaitForSeconds(cooldownFunc());
        fireWait = false;
    }

    protected virtual float cooldownFunc() { return stats.rateOfFire; }

    protected virtual void updateStart() { }
    protected virtual void updateEnd() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateStart();
        //TO DO add checkign line of fire for non homing projectiles, just copy from CustomAI and delete it there
        //fireWait = Math.Max(0.0f, fireWait - Time.deltaTime);
        if (target == null) return;
        distance = Vector2.Distance(transform.position, target.position);
        if (distance <= stats.range)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, stats.turretRotationSpeed * Time.deltaTime);
            if (!fireWait)
            {
                fire();
                distance = 0f;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, stats.turretRotationSpeed * Time.deltaTime);
        }
        updateEnd();

    }
}
