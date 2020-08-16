using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedMenu : MonoBehaviour
{

    public void Restart()
    {
        Debug.Log("Scene:" + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMainMenu()
    {
        Debug.Log("MenuScene");

        SceneManager.LoadScene("MenuScene");
    }
}