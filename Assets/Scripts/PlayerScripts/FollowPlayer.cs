using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    //public Transform player;
    public GameObject player;
    private Transform playerTransform;

    public EdgeCollider2D edges;

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
        playerTransform = player.GetComponent<Transform>();
        camera = GetComponent<Camera>();
        camera.orthographicSize = 16f - PlayerPrefs.GetFloat("camera_zoom", 0.5f) * (16f - 1f);

        isScaling = false;
        currentSize = maxSize;
        speedRange = maxSpeed - minSpeed;
        sizeDelta = (maxSize - minSize) / scaleTime;

        //player = GameObject.FindGameObjectWithTag("Player");
        //Debug.LogFormat("player found: {0}", player);


    }


    // Use this for initialization
    void Start () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);

        edges = GetComponentInChildren<EdgeCollider2D>(); 

        var bottomLeft = (Vector2)camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)) + Vector2.one * 0.3f;
        var topLeft = (Vector2)camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane)) + new Vector2(1,-1) * 0.3f;
        var topRight = (Vector2)camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane)) - Vector2.one * 0.3f;
        var bottomRight = (Vector2)camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, camera.nearClipPlane)) + new Vector2(-1,1) * 0.3f;

        var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
        edges.points = edgePoints;
        //Debug.LogFormat("camera, player: {0}", player.ToString());
    }

    // Update is called once per frame
    void Update () {

        if (playerTransform != null) transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 10f);

        

        


    }

}
