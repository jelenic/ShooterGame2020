using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    //public Transform player;
    public GameObject player;

    public float scaleTime;
    public float maxSize;  // max zoom out size of our camera
    public float minSize;  // min zoom out size of our camera
    public float minSpeed;  // min speed the camera cares about.. this or slower = min Zoomout
    public float maxSpeed;  // the max speed our camera careas about.. this or faster = max zoomout

    private Rigidbody2D playerRb;
    private Camera camera;
    private bool isScaling;
    private float currentSize;
    private float targetSize;
    private float speedRange; // this is used for lerp function
    private float sizeDelta;  // how long it takes to change the camera size 1 unit;


    void Awake()
    {
        scaleTime = 2f;
        maxSize = 12f;
        minSize = 5f;
        minSpeed = 8f;
        maxSpeed = 13f;
        playerRb = player.GetComponent<Rigidbody2D>();
        camera = GetComponent<Camera>();

        isScaling = false;
        currentSize = maxSize;
        speedRange = maxSpeed - minSpeed;
        sizeDelta = (maxSize - minSize) / scaleTime;

    }

    void CheckSize(float speed)
    {
        // clamp our speed between min and max
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        // normalize the speed between a value of 0 and 1 for lerp
        speed = speed - minSpeed;  // this sets our speed in the range from 0->speedRange
        speed /= speedRange;  // now speed is Normalized between 0->1

        targetSize = Mathf.Lerp(minSize, maxSize, speed);
        if (targetSize != currentSize)
            StartCoroutine(ChangeSize());
    }

    IEnumerator ChangeSize()
    {

        bool zoomOut = false;
        if (currentSize < targetSize)
            zoomOut = true;
        if (currentSize > targetSize)
            zoomOut = false;
        isScaling = true;
        while (currentSize != targetSize)
        {
            if (zoomOut)
            {
                currentSize += sizeDelta * Time.deltaTime;
                if (currentSize > targetSize)
                    currentSize = targetSize;
            }
            else
            {
                currentSize -= sizeDelta * Time.deltaTime;
                if (currentSize < targetSize)
                    currentSize = targetSize;
            }
            camera.orthographicSize = currentSize;
            yield return null;
        }
        isScaling = false;
    }

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);
        //Debug.LogFormat("camera, player: {0}", player.ToString());
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y,player.transform.position.z - 10f);

        //Debug.LogFormat("player speed: {0}", player.GetComponent<Rigidbody2D>().velocity.magnitude);



        //if we are currently scaling don't check movement
        //if (isScaling)
        //    return;
        //CheckSize(playerRb.velocity.magnitude);
    }

}
