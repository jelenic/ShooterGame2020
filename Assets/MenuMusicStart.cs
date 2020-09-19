using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicStart : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic("menu");
    }

}
