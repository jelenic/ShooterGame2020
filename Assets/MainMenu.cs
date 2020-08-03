using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        Debug.Log("Click Play");
        Loader.Load(Loader.Scene.GameScene);
    }

    public void Exit()
    {
        Debug.Log("EXITING");
        Application.Quit();
    }
}