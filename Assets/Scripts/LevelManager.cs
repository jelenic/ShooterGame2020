
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    #region LevelManagerSingelot
    public static LevelManager instance;
    public GameObject mobileControls;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of LevelManager found!");
        }
        instance = this;

        levels = Levels.instance;
        LevelDetails ld = levels.getCurrentLevelDetails();
        levelName = ld.name;
        levelDifficultyModifier = ld.difficultyModifier;


    }
    #endregion


    public GameObject pauseMenu;
    public GameObject finishMenu;
    public GameObject youDiedMenu;
    public GameObject settingsMenu;
    public GameObject scoreObject;
    public GameObject popupText;
    private bool wasPopupActive;
    private TextMeshProUGUI score;
    private Levels levels;

    public int currentScore;
    public string levelName;
    public float levelDifficultyModifier;
    public int levelScore;
    public bool levelOver = false;

    private void Start()
    {
        Time.timeScale = 1;
        score = scoreObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }

    private void togglePause()
    {

        if (Time.timeScale == 0) // if currently paused
        {
            if (pauseMenu != null) pauseMenu.SetActive(false);
            if (popupText != null && wasPopupActive)
            {
                wasPopupActive = false;
                popupText.SetActive(true);
            }
        }
        else
        {
            if (pauseMenu != null) pauseMenu.SetActive(true);
            if (popupText != null && popupText.activeSelf)
            {
                wasPopupActive = true;
                popupText.SetActive(false);
            }
        }

        if (settingsMenu != null) settingsMenu.SetActive(false);
        Time.timeScale = 1 - Time.timeScale;
    }

    public void Resume()
    {
        togglePause();
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
    
    public void NextLevel()
    {
        Levels.instance.nextLevel();

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

        AudioManager.instance.PlayMusic("victory");


        Debug.Log("level is finished");
        mobileControls.SetActive(false);
        finishMenu.SetActive(true);
        Time.timeScale = 0;
        ChangeTexts ct = finishMenu.GetComponent<ChangeTexts>();
        ct.completed(levelName);
        ct.scored(currentScore);
        int highscore = levels.scoreLevel(currentScore);
        ct.displayhighscore(highscore);


    }
    
    public void die()
    {
        levelOver = true;
        Debug.Log("level is ded");
        AudioManager.instance.PlayMusic("gameOver");

        mobileControls.SetActive(false);
        youDiedMenu.SetActive(true);
        Time.timeScale = 0;

    }

}
