using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedMenu : MonoBehaviour
{

    private void Awake()
    {
        Levels.instance.scoreLevel(0);
    }

    public void Restart()
    {
        Levels.instance.restartLevel();
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("MenuScene");

        Levels.instance.finishLevel();
    }
}