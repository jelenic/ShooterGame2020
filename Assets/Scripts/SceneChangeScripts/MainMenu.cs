using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void Play(string level)
    {
        Debug.Log("Click Play");
        Levels.instance.loadLevel(level);
    }

    public void Exit()
    {
        Debug.Log("EXITING");
        Application.Quit();
    }

    public void Save()
    {
        SaveManager.instance.save();
    }
    
    




}