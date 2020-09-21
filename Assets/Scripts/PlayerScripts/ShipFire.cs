using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFire : MonoBehaviour
{

    private bool fireWait;
    //private float timeTillFire;
    public Transform firepoint;
    private bool autofire;
    private Stats stats;
    private int activeWeapon;

    public GameObject w1;
    public GameObject w2;
    public Weapon w1_stats;
    public Weapon w2_stats;

    private Button toggleBtn;
    private Button switchBtn;

    public bool android;



    public void setWeapons(GameObject weapon1, Weapon weapon_stats1, GameObject weapon2, Weapon weapon_stats2)
    {
        w1 = weapon1;
        w2 = weapon2;
        w1_stats = weapon_stats1;
        w2_stats = weapon_stats2;
    }


    //public GameObject Bullets;
    //public GameObject MiniLaser;


    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        //timetillfire = stats.rateOfFire;
        autofire = false;

        activeWeapon = 1;
        //w1 = Bullets;
        //w2 = MiniLaser;

        if (Application.platform == RuntimePlatform.Android || android)
        {
            GameObject toggleBtnn = GameObject.Find("ToggleAttackBtn");
            toggleBtn = toggleBtnn.GetComponent<Button>();
            toggleBtn.onClick.AddListener(ToggleFire);


            GameObject switchBtnn = GameObject.Find("SwitchWeaponBtn");
            switchBtn = switchBtnn.GetComponent<Button>();
            switchBtn.onClick.AddListener(SwitchWeapons);
        }

    }

    private IEnumerator fireCooldown()
    {
        fireWait = true;
        yield return new WaitForSeconds(stats.rateOfFire * (activeWeapon == 1 ? w1_stats.rateOfFireModifier : w2_stats.rateOfFireModifier));
        fireWait = false;
    }

    // Update is called once per frame
    void Update()
    {
        //timetillfire -= Time.deltaTime;

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
    }

    void Shoot()
    {

        if (activeWeapon.Equals(1))
        {
            //Instantiate(w1, firepoint.position, firepoint.rotation).GetComponent<PlayerFiredBullet>().damageModifier = stats.calculateFinalDmgModifier();
            w1.GetComponent<WeaponFire>().Shoot();

        }
        else if (activeWeapon.Equals(2))
        {
            //Instantiate(w2, firepoint.position, firepoint.rotation).GetComponent<PlayerFiredBullet>().damageModifier = stats.calculateFinalDmgModifier();
            w2.GetComponent<WeaponFire>().Shoot();

        }
    }

    private void SwitchWeapons()
    {
        if (activeWeapon == 1)
        {
            activeWeapon = 2;
        }
        else
        {
            activeWeapon = 1;
        }
    }

    private void ToggleFire()
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

}
