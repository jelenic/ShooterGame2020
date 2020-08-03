using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 10f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x, player.position.y,player.position.z - 10f);
    }

}
