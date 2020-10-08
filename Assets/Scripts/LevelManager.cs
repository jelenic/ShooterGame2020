
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;


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
        levelDifficultyModifier = 1f;

        if (levels != null)
        {
            LevelDetails ld = levels.getCurrentLevelDetails();
            levelName = ld.name;
            levelDifficultyModifier = ld.difficultyModifier;
        }


    }
    #endregion

    #region UI_STUFF

    public GameObject youDiedMenu;
    public TextMeshProUGUI YOU_DIED;
    public GameObject gameDetails;
    public TextMeshProUGUI weaponDetailsText;
    public TextMeshProUGUI enemyDetailsText;
    public GameObject playerHPObject;
    public GameObject waveProgressDetails;



    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject scoreObject;
    public GameObject popupText;
    private bool wasPopupActive;
    private TextMeshProUGUI score;
    private Levels levels;

    #endregion

    public int currentScore;
    public string levelName;
    public float levelDifficultyModifier;
    public int levelScore;
    public bool levelOver = false;

    private SortedDictionary<string, int> killsPerWeapon = new SortedDictionary<string, int>();
    private SortedDictionary<string, int> killsPerEnemy = new SortedDictionary<string, int>();

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

    public void weaponKill(string weaponName = "")
    {
        if (!weaponName.Equals(""))
            if (!killsPerWeapon.ContainsKey(weaponName)) killsPerWeapon[weaponName] = 0;
            killsPerWeapon[weaponName]++;
    }
    

    public void increaseScore(int n, string enemyName = "")
    {
        if (!levelOver)
        {
            currentScore += n;
            score.text = "Score:" + currentScore.ToString();

            if (!enemyName.Equals(""))

                if (!killsPerEnemy.ContainsKey(enemyName)) killsPerEnemy[enemyName] = 0;
                    killsPerEnemy[enemyName]++;
        }
    }
    
    public void die()
    {
        levelOver = true;
        Debug.Log("level is ded");
        AudioManager.instance.PlayMusic("gameOver");
        //AudioManager.instance.PlayMusic("victory");


        int highscore = levels.scoreLevel(currentScore);


        mobileControls.SetActive(false);
        youDiedMenu.SetActive(true);
        popupText.SetActive(false);
        scoreObject.SetActive(false);
        playerHPObject.SetActive(false);
        waveProgressDetails.SetActive(false);

        Time.timeScale = 0;
        if (killsPerWeapon.Count > 0) StartCoroutine(showGameDetails());


    }

  
    private IEnumerator showGameDetails()
    {
        yield return new WaitForSecondsRealtime(2f);
        YOU_DIED.enabled = false;
        gameDetails.SetActive(true);

        string enemyKills = "";
        int i = 0;
        int j = 0;
        foreach(var s in killsPerEnemy.OrderBy(e => -e.Value))
        {
            enemyKills += string.Format("{0} : {1}\n", s.Key, s.Value);
            j += s.Value;
            if (++i == 5) break;
        }
        enemyDetailsText.text = string.Format("Enemies : {0}\n\n", j) + enemyKills;


        string weaponKills = "Weapons\n\n";
        i = 0;
        float totalWeaponKills = killsPerWeapon.Values.Sum();
        foreach (var s in killsPerWeapon.OrderBy(e => -e.Value))
        {
            weaponKills += string.Format("{0} : {1:0.00}%\n", s.Key, 100*s.Value / totalWeaponKills);
            if (i == 5) break;
        }
        weaponDetailsText.text = weaponKills;

    }

}
