using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void Play(string scene)
    {
        Debug.Log("Click Play");
        Loader.Load(scene);
    }

    public void Exit()
    {
        Debug.Log("EXITING");
        Application.Quit();
    }
}