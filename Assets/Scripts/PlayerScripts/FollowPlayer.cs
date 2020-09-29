using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    //public Transform player;
    public GameObject player;
    private Transform playerTransform;

    private Rigidbody2D playerRb;
    private Camera camera;

    void Awake()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
        camera = GetComponent<Camera>();
        camera.orthographicSize = 16f - PlayerPrefs.GetFloat("camera_zoom", 0.5f) * (16f - 1f);
    }


    // Use this for initialization
    void Start () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);
    }

    // Update is called once per frame
    void Update () {

        if (playerTransform != null) transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 10f);

        

        


    }

}
