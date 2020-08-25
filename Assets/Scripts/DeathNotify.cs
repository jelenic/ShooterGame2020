using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNotify : MonoBehaviour
{
    GameObject levelManager;
    CombatVariables cv;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        cv = GetComponent<CombatVariables>();
    }

    private void OnDestroy()
    {
        if (cv.hp == 0) levelManager.GetComponent<LevelManager>().finishLevel();
    }
    // Update is called once per frame
}
