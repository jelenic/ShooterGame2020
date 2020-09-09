using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    public GameObject finishMenu;
    public GameObject youDiedMenu;
    public GameObject scoreObject;
    private TextMeshProUGUI score;
    private Levels levels;

    public int currentScore;
    public string levelName;
    public int levelScore;
    public bool levelOver = false;

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
        Levels.instance.nextLevel();

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
        if (!levelOver)
        {
            currentScore += n;
            score.text = "Score:" + currentScore.ToString();
        }
    }

    public void finishLevel()
    {
        increaseScore(levelScore);

        levelOver = true;

        Debug.Log("level is finished");
        finishMenu.SetActive(true);
        Time.timeScale = 0;
        ChangeTexts ct = finishMenu.GetComponent<ChangeTexts>();
        ct.completed(levelName);
        ct.scored(currentScore);
        int highscore = levels.scoreLevel(currentScore);
        ct.displayhighscore(highscore);

    }

}
