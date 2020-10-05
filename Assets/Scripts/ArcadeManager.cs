using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    #region VARIABLES
    public bool arcadeActive;

    public int currentWave;

    private int lastSpecifiedWave;

    public GameObject[] enemies;
    public GameObject[] upgradeItems;

    [SerializeField]
    [Range(1, 50)]
    private int upgradesEveryNthWave;

    public WaveSettings[] waveSettings;

    public Vector2Int[] waveToEnemy;
    public Vector3[] waveToModifier;
    private Vector2Int waveRange;

    [SerializeField]
    [Range(1,50)]
    private int bossEveryNthWave;
    public GameObject[] bosses;


    [SerializeField]
    [Range(5, 1200)]
    private int waveLenght;

    [SerializeField]
    [Range(1, 300)]
    private int pauseLenght;

    private int currentWaveLenght;
    private int currentPauseLenght;

    public GameObject waveDetailsPopup;
    public TextMeshProUGUI popupText;

    public TextMeshProUGUI waveProgressText;
    public TextMeshProUGUI enemiesRemainingText;
    public Image enemiesRemainingBar;
    private int enemiesKilled;
    private int totalSpawnedEnemies;

    private Transform transform;
    public LevelManager levelManager;

    public GameObject bossDetails;
    public Image bossHP;
    public TextMeshProUGUI bossHPText;
    public TextMeshProUGUI bossName;
    private string boss_name;
    private bool bossAlive;

    private int currentBoss;

    public delegate void OnEnemySpawnStart(float spawnRate);
    public delegate void OnEnemySpawnStop();

    private int maxSpawnPerWave = 30;
    private int spawnedThisWave;

    public OnEnemySpawnStart OnEnemySpawnStartCallback;
    public OnEnemySpawnStop OnEnemySpawnStopCallback;

    public delegate void OnWaveStart();
    public delegate void OnWaveStop();

    public OnWaveStart OnWaveStartCallback;
    public OnWaveStop OnWaveStopCallback;

    #endregion


    #region ArcadeManagerSingelot
    public static ArcadeManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of ArcadeManager found!");
        }
        instance = this;
        #endregion
        levelManager = LevelManager.instance;

        transform = GetComponent<Transform>();
        enemiesKilled = 0;

        lastSpecifiedWave = waveSettings[waveSettings.Length - 1].maxWave + 1;
        waveToEnemy = new Vector2Int[lastSpecifiedWave];
        waveToModifier = new Vector3[lastSpecifiedWave];

        populateWaveToSettings();
        startWaves();
    }

    private void populateWaveToSettings()
    {
        foreach (WaveSettings w in waveSettings)
        {
            for (int j = w.minWave; j <= w.maxWave; j++)
            {
                waveToEnemy[j] = new Vector2Int(w.minEnemyRank, w.maxEnemyRank);
                waveToModifier[j] = new Vector3(w.difficultyModifier, w.spawnRate, w.maxSpawnPerWave);
            }
        }
    }

    private void startWaves()
    {
        StartCoroutine(arcadeLoop());
    }

    private IEnumerator arcadeLoop()
    {
        while (levelManager == null)
        {
            levelManager = LevelManager.instance;
            yield return new WaitForSeconds(0.1f);
        }
            
        yield return new WaitForSeconds(1f);

        float difficultyModifier = waveToModifier[Mathf.Min(currentWave, lastSpecifiedWave-1)].x;
        float spawnRate = waveToModifier[Mathf.Min(currentWave, lastSpecifiedWave-1)].y;


        for (int i = 0; i < currentWave / upgradesEveryNthWave; i++)
        {
            spawnUpgradeItems();
        }


        while (arcadeActive)
        {
            spawnedThisWave = 0;

            if (currentWave < waveToEnemy.Length)
            {
                waveRange = waveToEnemy[currentWave];
                difficultyModifier = waveToModifier[currentWave].x;
                spawnRate = waveToModifier[currentWave].y;
                maxSpawnPerWave = (int) waveToModifier[currentWave].z;
            }
            else
            {
                int maxRank = waveSettings[waveSettings.Length - 1].maxEnemyRank;
                waveRange = new Vector2Int(maxRank, maxRank - 1); // if currentWave is beyond what's specified only maxRank enemies spawn, -1 because the spawn function adds 1 to counter the range
                difficultyModifier += 0.25f; // if currentWave is beyond what's specified the difficulty modifier increases by 0.25f every wave
                spawnRate += 0.01f;
                maxSpawnPerWave += 5;
            }

            bool bossWave = ++currentWave % bossEveryNthWave == 0;
            if (bossWave) Debug.LogWarning("BOSS enemy!!!");
            Debug.LogFormat("current wave {0}, enemy range: {1}, difficlity: {2}", currentWave, waveRange, difficultyModifier);


            currentPauseLenght = currentWave == 1 ? 5 : pauseLenght;
            while (currentPauseLenght > 0)
            {
                string nextWaveText = bossWave ? string.Format("BOSS in {0}s", currentPauseLenght--) : string.Format("Wave {0} in {1}s", currentWave, currentPauseLenght--);
                waveProgressText.text = nextWaveText;
                yield return new WaitForSeconds(1f);
            }


            makePopup(string.Format("WAVE {0} STARTING!", currentWave), 1.5f);

            if (bossWave)
            {
                bossAlive = true;
                yield return new WaitForSeconds(2f);
                summonBoss();
            }

            if (levelManager != null) levelManager.levelDifficultyModifier *= difficultyModifier;
            if (!bossWave && OnEnemySpawnStartCallback != null) OnEnemySpawnStartCallback.Invoke(0.12f);
            if (OnWaveStartCallback != null) OnWaveStartCallback.Invoke();


            if (!bossWave)
            {
                currentWaveLenght = waveLenght;

                while (currentWaveLenght > 0 && !(spawnedThisWave >= maxSpawnPerWave && totalSpawnedEnemies > 0 && enemiesKilled == totalSpawnedEnemies))
                {
                    waveProgressText.text = string.Format("Wave {0} end in {1}s", currentWave, currentWaveLenght--);
                    yield return new WaitForSeconds(1f);
                }

                if (OnEnemySpawnStopCallback != null) OnEnemySpawnStopCallback.Invoke();

                makePopup(string.Format("WAVE {0} FINISHED!", currentWave), 1.5f);
            }
            else
            {
                while (bossAlive)
                {
                    yield return new WaitForSeconds(5f);
                }

            }
            if (OnWaveStopCallback != null) OnWaveStopCallback.Invoke();

            if ((currentWave % upgradesEveryNthWave) == 0) spawnUpgradeItems();
        }

    }

    #region SUMMON_METHODS
    public void summonRandomEnemy(Vector3 position)
    {
        spawnedThisWave++;
        int pickedEnemy = Random.Range(Mathf.Min(enemies.Length - 3, waveRange.x), Mathf.Min(enemies.Length - 1, waveRange.y + 1)); // to actually spawn all enemies in rank, to counter arrays starting from 0
        //Debug.LogFormat("summoning enemy {0} in range {1}, {2}, {3}", pickedEnemy, waveRange, Mathf.Min(enemies.Length - 1, waveRange.x), Mathf.Min(enemies.Length - 1, waveRange.y + 1));
        Instantiate(enemies[pickedEnemy], position, Quaternion.identity);

        if (spawnedThisWave >= maxSpawnPerWave && OnEnemySpawnStopCallback != null) OnEnemySpawnStopCallback.Invoke();

        enemySpawn();
    }
    private void summonBoss()
    {
        bossDetails.SetActive(true);
        Vector3 randomPos = 12f * Random.insideUnitCircle;
        randomPos += transform.position;
        Instantiate(bosses[currentBoss], randomPos, Quaternion.identity);
        currentBoss = (currentBoss + 1) % bosses.Length;

        enemySpawn();
    }

    private void spawnUpgradeItems()
    {
        int howMany = Random.Range(3, 5 + 1);

        for (int i = 0; i < howMany; i++)
        {
            int whichItem = Random.Range(0, upgradeItems.Length);
            Vector3 randomPos = 6f * Random.insideUnitCircle;
            randomPos += transform.position;
            Instantiate(upgradeItems[whichItem], randomPos, Quaternion.identity);
        }
    }

    #endregion

    public void enemyDeath()
    {
        if (enemiesRemainingBar != null && enemiesRemainingBar.IsActive())
        {
            enemiesKilled++;
            enemiesRemainingBar.fillAmount = (float)enemiesKilled / totalSpawnedEnemies;
            enemiesRemainingText.text = string.Format("{0} / {1}", enemiesKilled, totalSpawnedEnemies);
        }
    }

    private void enemySpawn()
    {
        totalSpawnedEnemies++;
        enemiesRemainingBar.fillAmount = (float) enemiesKilled / totalSpawnedEnemies;
        enemiesRemainingText.text = string.Format("{0} / {1}", enemiesKilled, totalSpawnedEnemies);
    }

    public void setBossDetails(string name)
    {
        boss_name = name;
        bossName.text = name;
        bossHP.fillAmount = 1f;
    }

    public void updateBossHP(int currentHP, int totalHP)
    {
        bossHP.fillAmount = (float) currentHP / totalHP;
        bossHPText.text = string.Format("{0} / {1}", currentHP, totalHP);
    }

    public void bossDeath()
    {
        bossName.text = "";
        bossHPText.text = "";
        bossDetails.SetActive(false);
        boss_name = "";
        bossAlive = false;
        enemyDeath();

        makePopup(string.Format("BOSS {0} DEFEATED!", boss_name), 1.5f);
    }

    private void makePopup(string message, float duration)
    {
        waveProgressText.text = "";
        waveDetailsPopup.SetActive(true);
        popupText.text = message;
        StartCoroutine(removePopup(duration));
    }

    private IEnumerator removePopup(float duration)
    {
        yield return new WaitForSeconds(duration);
        popupText.text = "";
        waveDetailsPopup.SetActive(false);
    }
}

[System.Serializable]
public class WaveSettings
{
    public int minWave;
    public int maxWave;
    public int minEnemyRank;
    public int maxEnemyRank;
    public float difficultyModifier;
    public float spawnRate;
    public int maxSpawnPerWave;
}
