using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : ModuleScript
{
    // Start is called before the first frame update
    private float remainingCooldown;
    private float remainingTime;
    private GameObject ship;
    private GameObject shield;
    private bool active;

    void Start()
    {
        remainingCooldown = coooldown;
        active = false;
        ship = GameObject.FindGameObjectWithTag("Player");
        shield = gameObject.transform.Find("Shield").gameObject;

    }



    // Update is called once per frame
    void Update()
    {
        remainingCooldown -= Time.deltaTime;
        remainingTime -= Time.deltaTime;
        if (remainingCooldown <= 0 && Input.GetMouseButtonDown(1))
        {
            remainingCooldown = coooldown + duration;
            ship.GetComponent<CombatVariables>().involunrable = true;
            remainingTime = duration;
            shield.SetActive(true);
            active = true;
            Debug.Log("shield active");
            
        }
        if (remainingTime <= 0 && active)
        {
            ship.GetComponent<CombatVariables>().involunrable = false;
            shield.SetActive(false);
            active = false;
        }
    }
}
