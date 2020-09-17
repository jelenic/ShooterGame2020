using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
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

    public int scoreValue { get => (int) (difficultyModifier * _scoreValue); set => _scoreValue = value; }
    public float range { get => difficultyModifier *  _range; set => range = value; }
    public float angleSpeed { get => difficultyModifier *  _angleSpeed; set => _angleSpeed = value; }
    public float rateOfFire { get => difficultyModifier *  _rateOfFire; set => _rateOfFire = value; }
    public float turretRotationSpeed { get => difficultyModifier *  _turretRotationSpeed; set => _turretRotationSpeed = value; }
    public float speed { get => difficultyModifier * _speed; set => _speed = value; }
    public int hp { get => (int)(difficultyModifier * _hp); set => _hp = value; }
    public float projectileResistance { get => difficultyModifier * _projectileResistance; set => _projectileResistance = value; }
    public float beamResistance { get => difficultyModifier * _beamResistance; set => _beamResistance = value; }
    public float physicalResistance { get => difficultyModifier * _physicalResistance; set => _physicalResistance = value; }
    public float damageModifier { get => difficultyModifier * _damageModifier; set => _damageModifier = value; }
    public float critChance { get => difficultyModifier * _critChance; set => _critChance = value; }
    public float critMultiplier { get => difficultyModifier * _critMultiplier; set => _critMultiplier = value; }
    public float projectileVelocityModifier { get => difficultyModifier * _projectileVelocityModifier; set => _projectileVelocityModifier = value; }

    private float difficultyModifier = 1f;

    //player only
    public float maxVelocity;
    public float thrust;


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
            Debug.Log(difficultyModifier + " orewa enemy da! " + name);
        }
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
}
}
