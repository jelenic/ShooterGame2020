using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFireArcade : MonoBehaviour
{

    private bool fireWait;
    //private float timeTillFire;
    private bool autofire;
    private Stats stats;
    private int activeWeapon;


    public GameObject weapon1Object;
    public GameObject weapon2Object;
    public GameObject weapon3Object;

    private BasicWeaponArcade weapon1;
    private BasicWeaponArcade weapon2;
    private BasicWeaponArcade weapon3; 


    private float damageModifier;

    private Button specialAttack;
    private Button switchBtn;

    public bool android;


    private IEnumerator fireCooldown()
    {
        fireWait = true;
        yield return new WaitForSeconds(stats.rateOfFire * (activeWeapon == 1 ? weapon1.rateOfFire : (activeWeapon == 2 ? weapon2.rateOfFire : weapon3.rateOfFire)));
        fireWait = false;
    }

    public void weaponUpgrade()
    {
        if (activeWeapon == 1)
        {
            weapon1.upgrade();
        }
        else if (activeWeapon == 2)
        {
            weapon2.upgrade();
        }
        else
        {
            weapon3.upgrade();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        weapon1 = Instantiate(weapon1Object).GetComponent<BasicWeaponArcade>();
        weapon2 = Instantiate(weapon2Object).GetComponent<BasicWeaponArcade>();
        weapon3 = Instantiate(weapon3Object).GetComponent<BasicWeaponArcade>();

        weapon1.setPlayerTransform(transform);
        weapon2.setPlayerTransform(transform);
        weapon3.setPlayerTransform(transform);

        stats = GetComponent<Stats>();
        damageModifier = stats.damageModifier;
        activeWeapon = 1;

        if (Application.platform == RuntimePlatform.Android || android)
        {
            /*GameObject toggleBtnn = GameObject.Find("ToggleAttackBtn");
            specialAttack = toggleBtnn.GetComponent<Button>();
            specialAttack.onClick.AddListener(SpecialAttack);*/


            GameObject switchBtnn = GameObject.Find("SwitchWeaponBtn");
            switchBtn = switchBtnn.GetComponent<Button>();
            switchBtn.onClick.AddListener(SwitchWeapons);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (autofire)
            {
                autofire = false;
            }
            else
            {
                autofire = true;
            }
        }

        if (!fireWait && autofire)
        {
            Shoot();
            StartCoroutine(fireCooldown());
            //Debug.Log(stats.rateOfFire);
            //Debug.Log(timetillfire.ToString());
            //timetillfire = stats.rateOfFire * (activeWeapon == 1 ? w1_stats.rateOfFireModifier : w2_stats.rateOfFireModifier);

        }

        if (Input.GetKeyDown("1"))
        {
            activeWeapon = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            activeWeapon = 2;
        }
        else if (Input.GetKeyDown("3"))
        {
            activeWeapon = 3;
        }
    }

    void Shoot()
    {

        if (activeWeapon == 1)
        {
            weapon1.fire(stats.calculateFinalDmgModifier(), stats.projectileVelocityModifier);
        }
        else if (activeWeapon == 2)
        {
            weapon2.fire(stats.calculateFinalDmgModifier(), stats.projectileVelocityModifier);
        }
        else 
        {
            weapon3.fire(stats.calculateFinalDmgModifier(), stats.projectileVelocityModifier);
        }
    }

    private void SwitchWeapons()
    {
        if (activeWeapon == 1)
        {
            activeWeapon = 2;
        }
        else if (activeWeapon == 2)
        {
            activeWeapon = 3;
        }
        else
        {
            activeWeapon = 1;
        }
    }


}
