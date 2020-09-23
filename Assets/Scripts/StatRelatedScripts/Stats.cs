using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public OriginalStats og;
    private LevelManager levelManager;

    public new string name;

    public float mass;


    //defenses related
    [SerializeField]
    private int _hp;

    [SerializeField]
    private float _projectileResistance;

    [SerializeField]
    private float _beamResistance;

    [SerializeField]
    private float _physicalResistance;

    //damage related
    [SerializeField]
    private float _damageModifier;

    [SerializeField]
    private float _critChance;

    [SerializeField]
    private float _critMultiplier;

    [SerializeField]
    private float _projectileVelocityModifier;

    //enemy only
    [SerializeField]
    private int _scoreValue;

    [SerializeField]
    private float _range;


    [SerializeField]
    private float _angleSpeed;


    [SerializeField]
    private float _rateOfFire;


    [SerializeField]
    private float _turretRotationSpeed;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _stoppingDistance;


    public int scoreValue { get => (int) Mathf.Clamp((difficultyModifier * _scoreValue), 0f, 10000f); set => _scoreValue = value; }
    public float range { get => Mathf.Clamp( difficultyModifier *  _range, 8f, 1000f); set => _range = value; }
    public float angleSpeed { get => Mathf.Clamp( difficultyModifier *  _angleSpeed, 0f, 30f); set => _angleSpeed = value; }
    public float rateOfFire { get => Mathf.Clamp(  _rateOfFire / difficultyModifier, 0f, 10f); set => _rateOfFire = value; }
    public float turretRotationSpeed { get => Mathf.Clamp( difficultyModifier *  _turretRotationSpeed, 0f, 30f); set => _turretRotationSpeed = value; }
    public float speed { get => Mathf.Clamp( difficultyModifier * _speed, 0f, 50000f); set => _speed = value; }
    public int hp { get => (int) Mathf.Clamp((difficultyModifier * _hp), 20f, 50000f); set => _hp = value; }
    public float projectileResistance { get => Mathf.Clamp( difficultyModifier * _projectileResistance, 0f, 1f); set => _projectileResistance = value; }
    public float beamResistance { get => Mathf.Clamp( difficultyModifier * _beamResistance, 0f, 1f); set => _beamResistance = value; }
    public float physicalResistance { get => Mathf.Clamp( difficultyModifier * _physicalResistance, 0f, 1f); set => _physicalResistance = value; }
    public float damageModifier { get => Mathf.Clamp( difficultyModifier * _damageModifier, 0.1f, 10f); set => _damageModifier = value; }
    public float critChance { get => Mathf.Clamp( difficultyModifier * _critChance, 0f, 1f); set => _critChance = value; }
    public float critMultiplier { get => Mathf.Clamp( difficultyModifier * _critMultiplier, 1f, 5f); set => _critMultiplier = value; }
    public float projectileVelocityModifier { get => Mathf.Clamp( difficultyModifier * _projectileVelocityModifier, 0.1f, 15f); set => _projectileVelocityModifier = value; }
    public float stoppingDistance { get => _stoppingDistance; set => _stoppingDistance = value; }

    private float difficultyModifier = 1f;

    //player only
    public float maxVelocity;
    public float thrust;
    public int magazineModifier;


    public float calculateFinalDmgModifier()
    {
        return (Random.value <= critChance ? critMultiplier : 1) * damageModifier;
    }

    private void Start()
    {
        levelManager = LevelManager.instance;

        if (gameObject.CompareTag("Enemy"))
        {
            difficultyModifier = levelManager.levelDifficultyModifier;

        }

        GetComponent<Rigidbody2D>().mass = mass;

        refreshOriginal();
    }

    public void refreshOriginal()
    {
        og = new OriginalStats(this);

    }

}



public struct OriginalStats // for making duplicates which is annoyingly hard with monobehaviour classes
{
    public int hp;
    public float mass;
    public float projectileResistance;
    public float beamResistance;
    public float physicalResistance;
    public float damageModifier;
    public float critChance;
    public float critMultiplier;
    public float projectileVelocityModifier;
    public int scoreValue;
    public float range;
    public float angleSpeed;
    public float rateOfFire;
    public float turretRotationSpeed;
    public float speed;
    public float maxVelocity;
    public float thrust;
    public int magazineModifier;
    public OriginalStats(Stats stats)
    {
        this.hp = stats.hp;
        this.mass = stats.mass;
        this.projectileResistance = stats.projectileResistance;
        this.beamResistance = stats.beamResistance;
        this.physicalResistance = stats.physicalResistance;
        this.damageModifier = stats.damageModifier;
        this.critChance = stats.critChance;
        this.critMultiplier = stats.critMultiplier;
        this.projectileVelocityModifier = stats.projectileVelocityModifier;
        this.scoreValue = stats.scoreValue;
        this.range = stats.range;
        this.angleSpeed = stats.angleSpeed;
        this.rateOfFire = stats.rateOfFire;
        this.turretRotationSpeed = stats.turretRotationSpeed;
        this.speed = stats.speed;
        this.maxVelocity = stats.maxVelocity;
        this.thrust = stats.thrust;
        this.magazineModifier = stats.magazineModifier;
}
}
