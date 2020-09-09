using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;

public class ShipMovement : MonoBehaviour {

    private Joystick joystickrotate;
    private Joystick joystickspeed;
    private Rigidbody2D rb;
    private float thrust;
    private float maxVelocity;
    private Stats stats;
    //public Transform firepoint;
    //public GameObject Bullets;

    private GameObject YouDiedMenu;
    private GameObject MobileControlMenu;





    Vector2 mouseWorldPosition;
    Vector2 direction;

    // Use this for initialization
    void Start() {
        stats = GetComponent<Stats>();
        thrust = 15f;
        rb = GetComponent<Rigidbody2D>();
        maxVelocity = 50;
        YouDiedMenu = GameObject.Find("YouDiedMenu");
        MobileControlMenu = GameObject.Find("MobileControlls");
        YouDiedMenu.SetActive(false);
        if (Application.platform != RuntimePlatform.Android && true)
        {
            MobileControlMenu.SetActive(false);
        }
        else
        {
            //Debug.Log(joystickrotate);
            GameObject joystickSpeed = GameObject.Find("JoystickMovement");
            GameObject joystickRotate = GameObject.Find("JoystickRotation");
            //Debug.Log(joystickRotate);
            joystickrotate = joystickRotate.GetComponent<Joystick>();
            joystickspeed = joystickSpeed.GetComponent<Joystick>();
            //Debug.Log(joystickrotate);
        }
        //Debug.Log(MobileControlMenu.ToString());
    }

    // Update is called once per frame
    void Update() {

        if (Application.platform == RuntimePlatform.Android || false)
        {
            if (joystickrotate.Vertical != 0 && joystickrotate.Horizontal != 0)
            {
                float angle = Mathf.Atan2(joystickrotate.Horizontal, joystickrotate.Vertical) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, -angle);
            }
            if (joystickspeed.Horizontal != 0 && joystickspeed.Vertical != 0)
            {
                float angleSpeed = Mathf.Atan2(joystickspeed.Horizontal, joystickspeed.Vertical) * Mathf.Rad2Deg;
                Vector3 dir = Quaternion.Euler(0f, 0f, -angleSpeed) * Vector3.up;
                rb.AddForce(dir * stats.thrust);
            }
        }

        ClampVelocity();


        if (Application.platform != RuntimePlatform.Android && true)
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

    private void OnDestroy()
    {
        YouDiedMenu.SetActive(true);
    }


}
