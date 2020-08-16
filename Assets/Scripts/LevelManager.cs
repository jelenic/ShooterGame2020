using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public GameObject finishMenu;
    public GameObject levelScore;
    public string levelName;

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
    
    public void NextLevel()
    {
        Debug.Log("NextScene");

    }


    private void Start()
    {
        Time.timeScale = 1;

    }

    public void finishLevel()
    {
        Debug.Log("level is finished");
        finishMenu.SetActive(true);
        Time.timeScale = 0;
        ChangeTexts ct = finishMenu.GetComponent<ChangeTexts>();
        ct.completed(levelName);
        ct.scored(levelScore.GetComponent<Score>().currentScore);

    }
}
