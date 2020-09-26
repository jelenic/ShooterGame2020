using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    public GameObject[] enemies;
    public WaveSettings[] waveSettings;

    public Vector3[] waveToEnemy;
    private Vector2Int waveRange;

    [SerializeField]
    [Range(2,50)]
    private int bossEveryNthWave;
    public GameObject[] bosses;

    public bool arcadeActive;

    private int currentWave;

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

    public delegate void OnWaveStart(int wave);
    public delegate void OnWaveEnd();

    public OnWaveStart OnWaveStartedCallback;
    public OnWaveEnd OnWaveEndedCallback;

    private LevelManager levelManager;

    public GameObject bossDetails;
    public Image bossHP;
    public TextMeshProUGUI bossHPText;
    public TextMeshProUGUI bossName;

    private int currentBoss;

    private bool waveOnGoing;

    private string boss_name;
    private bool bossAlive;

    private Transform transform;


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
        startWaves();
        populateWaveToEnemy();
    }

    private void summonBoss()
    {
        bossDetails.SetActive(true);
        Vector3 randomPos = 12f * Random.insideUnitCircle;
        randomPos += transform.position;
        Instantiate(bosses[currentBoss], randomPos , Quaternion.identity);
        currentBoss = (currentBoss + 1) % bosses.Length;
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
        waveOnGoing = false;
        boss_name = "";
        bossAlive = false;

        makePopup(string.Format("BOSS {0} DEFEATED!", boss_name), 1.5f);
    }

    private void populateWaveToEnemy()
    {
        foreach(WaveSettings w in waveSettings)
        {
            for(int j = w.minWave; j <= w.maxWave; j++)
            {
                waveToEnemy[j] = new Vector3(w.minEnemyRank, w.maxEnemyRank, w.difficultyModifier);
            }
        }
    }

    public void summonRandomEnemy(Vector3 position)
    {
        
        int pickedEnemy = Random.Range(waveRange.x, waveRange.y + 1); // to actually spawn all enemies in rank, to counter arrays starting from 0
        Debug.Log(pickedEnemy + " summoning enemy in range " + waveRange);
        Instantiate(enemies[pickedEnemy], position, Quaternion.identity);
    }

    private void startWaves()
    {
        StartCoroutine(arcadeLoop());
    }


    private IEnumerator arcadeLoop()
    {
        yield return new WaitForSeconds(1f);

        float difficultyModifier = 1f;
        while (arcadeActive)
        {
            if (currentWave < waveToEnemy.Length)
            {
                waveRange = new Vector2Int((int) waveToEnemy[currentWave].x, (int)waveToEnemy[currentWave].y);
                difficultyModifier = waveToEnemy[currentWave].z;
            } else
            {
                int maxRank = waveSettings[waveSettings.Length - 1].maxEnemyRank; 
                waveRange = new Vector2Int(maxRank, maxRank-1); // if currentWave is beyond what's specified only maxRank enemies spawn, -1 because the spawn function adds 1 to counter the range
                difficultyModifier = 2f + (currentWave - waveToEnemy.Length) * 0.03f; // if currentWave is beyond what's specified the difficulty modifier increases by 0.03f every wave
            }

            bool bossWave = ++currentWave % bossEveryNthWave == 0;
            if (bossWave) Debug.LogWarning("BOSS enemy!!!");
            Debug.LogFormat("current wave {0}, enemy range: {1}, difficlity: {2}", currentWave, waveRange, difficultyModifier);


            currentPauseLenght = pauseLenght;
            while (currentPauseLenght > 0)
            {
                string nextWaveText = bossWave ? string.Format("BOSS in {0}s", currentPauseLenght--) : string.Format("Wave {0} in {1}s", currentWave, currentPauseLenght--);
                waveProgressText.text = nextWaveText;
                yield return new WaitForSeconds(1f);
            }


            waveOnGoing = true;
            makePopup(string.Format("WAVE {0} STARTING!", currentWave), 1.5f);

            if (bossWave)
            {
                bossAlive = true;
                yield return new WaitForSeconds(2f);
                summonBoss();
            }


            levelManager.levelDifficultyModifier *= 1.03f;
            if (!bossWave && OnWaveStartedCallback != null) OnWaveStartedCallback.Invoke(currentWave);

            
            if (!bossWave)
            {
                currentWaveLenght = waveLenght;
                while (currentWaveLenght > 0 && waveOnGoing)
                {
                    waveProgressText.text = string.Format("Wave {0} end in {1}s", currentWave, currentWaveLenght--);
                    yield return new WaitForSeconds(1f);
                }

                if (OnWaveEndedCallback != null) OnWaveEndedCallback.Invoke();

                makePopup(string.Format("WAVE {0} FINISHED!", currentWave), 1.5f);
            } else
            {
                while (bossAlive)
                {
                    yield return new WaitForSeconds(5f);
                }
            }
        }

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
}
