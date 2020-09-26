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

    public GameObject ammo1;
    public GameObject ammo2;
    public GameObject ammo3;

    private float damageModifier;
    private float rateOfFire;

    private Button specialAttack;
    private Button switchBtn;

    public bool android;


    private IEnumerator fireCooldown()
    {
        fireWait = true;
        float returnValue;
        if (activeWeapon == 1)
        {
            returnValue = rateOfFire / 5;
        }
        else if (activeWeapon == 2)
        {
            returnValue = rateOfFire * 2;
        }
        else
        {
            returnValue = rateOfFire;
        }
        //yield return new WaitForSeconds(activeWeapon == 1 ? rateOfFire/5 : rateOfFire*2);
        yield return new WaitForSeconds(returnValue);
        fireWait = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        damageModifier = stats.damageModifier;
        activeWeapon = 1;
        rateOfFire = stats.rateOfFire;

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
            PlayerFiredBullet pfb = Instantiate(ammo1, transform.position, transform.rotation).GetComponent<PlayerFiredBullet>();
            pfb.damageModifier = stats.calculateFinalDmgModifier() * damageModifier*0.5f;
            Debug.Log(pfb.damageModifier);
            pfb.velocityModifier = stats.projectileVelocityModifier;
            Debug.Log(pfb.velocityModifier);

        }
        else if (activeWeapon == 2)
        {
            PlayerFiredMine pfm = Instantiate(ammo2, transform.position + transform.up, transform.rotation).GetComponent<PlayerFiredMine>();
            pfm.damageModifier = stats.calculateFinalDmgModifier() * damageModifier;

        }
        else 
        {
            PlayerFiredBullet pfb = Instantiate(ammo3, transform.position + transform.up, transform.rotation).GetComponent<PlayerFiredBullet>();
            PlayerFiredBullet pfb2 = Instantiate(ammo3, transform.position + transform.up + transform.right * -0.5f, transform.rotation * Quaternion.Euler(0f, 0f, 15f)).GetComponent<PlayerFiredBullet>();
            PlayerFiredBullet pfb3 = Instantiate(ammo3, transform.position + transform.up + transform.right*0.5f, transform.rotation * Quaternion.Euler(0f, 0f, -15f)).GetComponent<PlayerFiredBullet>();
            pfb.damageModifier = stats.calculateFinalDmgModifier() * damageModifier*2f;
            pfb.velocityModifier = stats.projectileVelocityModifier;
            pfb2.damageModifier = stats.calculateFinalDmgModifier() * damageModifier*2f;
            pfb2.velocityModifier = stats.projectileVelocityModifier;
            pfb3.damageModifier = stats.calculateFinalDmgModifier() * damageModifier*2f;
            pfb3.velocityModifier = stats.projectileVelocityModifier;

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
