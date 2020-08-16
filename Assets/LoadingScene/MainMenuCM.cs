using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenuCM : MonoBehaviour {

    private void Awake() {
        transform.Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            Loader.Load(Loader.Scene.GameScene.ToString());
        };
    }

}
