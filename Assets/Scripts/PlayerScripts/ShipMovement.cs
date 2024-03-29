﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.UIElements;

public class ShipMovement : MonoBehaviour {

    private Joystick joystickrotate;
    private Joystick joystickspeed;
    private Rigidbody2D rb;
    private Stats stats;

    public bool android;
    public bool clamp;
    //public GameObject MobileControlMenu;

    public bool boostUsed;
    public float boostVelThreshold;
    public float boostValue;
    public float boostCooldown;
    public float iframesDuration;

    
    public float energyUse;
    public float currentEnergy;
    public int recharge;
    public bool enoughEnergy;

    public Image energyBar;

    IEnumerator boostCD()
    {
        yield return new WaitForSeconds(0.25f*boostCooldown);
        boostUsed = false;
    }
    IEnumerator energyCD()
    {
        yield return new WaitForSeconds(boostCooldown);
        recharge--;
    }

    IEnumerator iframes()
    {
        gameObject.layer = 16;
        //foreach (Collider2D c in colliders) c.enabled = false;
        yield return new WaitForSeconds(iframesDuration);
        //foreach (Collider2D c in colliders) c.enabled = true;
        gameObject.layer = 11;

    }
    
    private void energyCalculation()
    {
        currentEnergy -= energyUse;
        if (energyBar != null) energyBar.fillAmount = currentEnergy / stats.totalEnergy;

        if ((currentEnergy - energyUse) < 0f)
        {
            enoughEnergy = false;
        }
        else
        {
            enoughEnergy = true;
        }
    }





    Vector2 mouseWorldPosition;
    Vector2 direction;

    // Use this for initialization
    void Start() {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        //if(MobileControlMenu != null) MobileControlMenu = GameObject.Find("MobileControlls");
        if (Application.platform != RuntimePlatform.Android && !android)
        {
            //MobileControlMenu.SetActive(false);
        }
        else
        {
            //Debug.Log(joystickrotate);
            GameObject joystickSpeed = GameObject.Find("JoystickMovement");
            //GameObject joystickRotate = GameObject.Find("JoystickRotation");
            //Debug.Log(joystickRotate);
            //joystickrotate = joystickRotate.GetComponent<Joystick>();
            joystickspeed = joystickSpeed.GetComponent<Joystick>();
            android = true;
            //Debug.Log(joystickrotate);
        }

        currentEnergy = stats.totalEnergy;
        enoughEnergy = true;
        //Debug.Log(MobileControlMenu.ToString());
    }



    

    // Update is called once per frame
    void FixedUpdate() {
        if (recharge == 0)
        {
            currentEnergy = Mathf.Min(stats.totalEnergy, currentEnergy + Time.deltaTime * stats.energyRechargeSpeed);
            if (energyBar != null) energyBar.fillAmount = currentEnergy / stats.totalEnergy;
            if ((currentEnergy - energyUse) > 0f)
            {
                enoughEnergy = true;
            }
        }


        if (android || Application.platform == RuntimePlatform.Android)
        {
            
            Vector3 movement = new Vector3(joystickspeed.Horizontal, joystickspeed.Vertical, 0f);
            float magnitude = movement.magnitude;

            if (!magnitude.Equals(0f))
            {
                float angle = Mathf.Atan2(joystickspeed.Horizontal, joystickspeed.Vertical) * Mathf.Rad2Deg;

                if (magnitude <= 0.5f)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -angle);
                }
                else
                {
                    Vector3 dir = Quaternion.Euler(0f, 0f, -angle) * Vector3.up * magnitude;
                    transform.rotation = Quaternion.Euler(0f, 0f, -angle);
                    rb.AddForce(dir * stats.thrust);
                }
            }


            //if (joystickrotate.Vertical != 0 && joystickrotate.Horizontal != 0)
            //{
            //    float angle = Mathf.Atan2(joystickrotate.Horizontal, joystickrotate.Vertical) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.Euler(0f, 0f, -angle);
            //}
            //if (joystickspeed.Horizontal != 0 && joystickspeed.Vertical != 0)
            //{
            //    Vector3 movement = new Vector3(joystickspeed.Horizontal, joystickspeed.Vertical, 0f);
            //    float angleSpeed = Mathf.Atan2(joystickspeed.Horizontal, joystickspeed.Vertical) * Mathf.Rad2Deg;
            //    Vector3 dir = Quaternion.Euler(0f, 0f, -angleSpeed) * Vector3.up * movement.magnitude;
            //    rb.AddForce(dir * stats.thrust);
            //}
        }

        if (clamp) ClampVelocity();


        if (!android)
        {
            
            if (Input.GetKey("w"))
            {
                Vector2 force = Vector3.up * stats.thrust;
                rb.AddForce(force);
            }

            if (Input.GetKey("s"))
            {
                Vector2 force = Vector3.down * stats.thrust;
                rb.AddForce(force);
                //Debug.Log("s");
            }


            if (Input.GetKey("a"))
            {
                Vector2 force = Vector3.left * stats.thrust;
                rb.AddForce(force);
            }

            if (Input.GetKey("d"))
            {
                Vector2 force = Vector3.right * stats.thrust;
                rb.AddForce(force);
            }

            if (!boostUsed && Input.GetKey(KeyCode.LeftShift))
            {
                if (enoughEnergy && rb.velocity.magnitude >= boostVelThreshold)
                {
                    energyCalculation();
                    StartCoroutine(iframes());
                    boostUsed = true;
                    recharge++;
                    Vector2 force = rb.velocity.normalized * stats.thrust * boostValue;
                    rb.AddForce(force);
                    StartCoroutine(boostCD());
                    StartCoroutine(energyCD());
                } 

            }
            //rotate to mouse
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get direction you want to point at
            direction = (mouseWorldPosition - (Vector2)transform.position).normalized;

            // set vector of transform directly
            transform.up = direction;
        }


    }

    private void ClampVelocity(){
        float x = Mathf.Clamp(rb.velocity.x, -stats.maxVelocity, stats.maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -stats.maxVelocity, stats.maxVelocity);
        rb.velocity = new Vector2(x, y);
        //Debug.Log(y.ToString());
    }



}
