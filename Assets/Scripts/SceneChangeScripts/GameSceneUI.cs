using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameSceneUI : MonoBehaviour {

    public void MainMenu()
    {
        Debug.Log("Click Main Menu");
        Loader.Load(Loader.Scene.MenuScene.ToString());
    }
}
