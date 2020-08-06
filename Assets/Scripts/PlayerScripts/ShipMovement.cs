using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;

public class ShipMovement : MonoBehaviour {

    //public Joystick joystickrotate;
    //public Joystick joystickspeed;
    private Rigidbody2D rb;
    private float thrust;
    private float maxVelocity;
    //public Transform firepoint;
    //public GameObject Bullets;





    Vector2 mouseWorldPosition;
    Vector2 direction;

    // Use this for initialization
    void Start() {
        thrust = 15f;
        rb = GetComponent<Rigidbody2D>();
        maxVelocity = 50;
    }

    // Update is called once per frame
    void Update() {
        /*
        if (joystickrotate.Vertical != 0 && joystickrotate.Horizontal != 0)
        {
            float angle = Mathf.Atan2(joystickrotate.Horizontal, joystickrotate.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
        if (joystickspeed.Horizontal !=0 && joystickspeed.Vertical != 0)
        {
            float angleSpeed = Mathf.Atan2(joystickspeed.Horizontal, joystickspeed.Vertical) * Mathf.Rad2Deg;
            Vector3 dir = Quaternion.Euler(0f, 0f, -angleSpeed)*Vector3.up;
            rb.AddForce(dir * thrust * Time.deltaTime);
        }*/

        ClampVelocity();


        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetKey("w"))
            {
                Vector2 force = Vector3.up * thrust;
                rb.AddForce(force);
            }

            if (Input.GetKey("s"))
            {
                Vector2 force = Vector3.down * thrust;
                rb.AddForce(force);
                //Debug.Log("s");
            }


            if (Input.GetKey("a"))
            {
                Vector2 force = Vector3.left * thrust;
                rb.AddForce(force);
            }

            if (Input.GetKey("d"))
            {
                Vector2 force = Vector3.right * thrust;
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
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
        //Debug.Log(y.ToString());
    }


}
