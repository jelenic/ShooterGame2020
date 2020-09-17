using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNotify : MonoBehaviour
{
    LevelManager levelManager;
    CombatVariables cv;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        cv = GetComponent<CombatVariables>();
    }

    private void OnDestroy()
    {
        if (cv.hp == 0) levelManager.finishLevel();
    }
    // Update is called once per frame
}
