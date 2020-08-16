using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNotify : MonoBehaviour
{
    GameObject levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    private void OnDestroy()
    {
        levelManager.GetComponent<LevelManager>().finishLevel();
    }
    // Update is called once per frame
}
