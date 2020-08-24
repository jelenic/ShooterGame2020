using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    public GameObject finishMenu;
    public GameObject scoreObject;
    private TextMeshProUGUI score;
    private Levels levels;

    public int currentScore;
    public string levelName;

    public void Restart()
    {
        Debug.Log("Scene:" + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMainMenu()
    {
        Debug.Log("MenuScene");
        Levels.instance.finishLevel();

    }
    
    public void NextLevel()
    {
        Debug.Log("NextScene");

    }

    private void Awake()
    {
        levels = Levels.instance;
    }

    private void Start()
    {
        Time.timeScale = 1;
        score = scoreObject.GetComponent<TextMeshProUGUI>();
        levelName = levels.currentlyPlayed;
    }

    public void increaseScore(int n)
    {
        currentScore += n;
        score.text = "Score:" + currentScore.ToString();
    }

    public void finishLevel()
    {
        Debug.Log("level is finished");
        finishMenu.SetActive(true);
        Time.timeScale = 0;
        ChangeTexts ct = finishMenu.GetComponent<ChangeTexts>();
        ct.completed(levelName);
        ct.scored(currentScore);
        levels.scoreLevel(currentScore);

    }
}
