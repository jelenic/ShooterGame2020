using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerDetails : MonoBehaviour
{
    GameObject player;
    TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<TextMeshPro>();
        text.SetText(player.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
