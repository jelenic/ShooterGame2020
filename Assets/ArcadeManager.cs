using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    public GameObject[] enemies;
    public WaveSettings[] waveSettings;

    public Vector2Int[] waveToEnemy;
    private Vector2Int waveRange;

    [SerializeField]
    [Range(2,50)]
    private int bossEveryNthWave;
    public GameObject[] bosses;


    public bool arcadeActive;

    private int currentWave;

    [SerializeField]
    [Range(10, 1200)]
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

        startWaves();
        populateWaveToEnemy();
    }

    private void populateWaveToEnemy()
    {
        foreach(WaveSettings w in waveSettings)
        {
            for(int j = w.minWave; j <= w.maxWave; j++)
            {
                waveToEnemy[j] = new Vector2Int(w.minEnemyRank, w.maxEnemyRank);
            }
        }
    }

    

    public void summonRandomEnemy(Vector3 position)
    {
        
        int pickedEnemy = Random.Range(waveRange.x, waveRange.y + 1);
        Debug.Log(pickedEnemy + " summoning enemy in range " + waveRange);
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
            if (currentWave < waveSettings.Length)
            {
                waveRange = waveToEnemy[currentWave];
                difficultyModifier = waveSettings[currentWave].difficultyModifier;
            } else
            {
                int maxRank = waveSettings[waveSettings.Length - 1].maxEnemyRank;
                waveRange = new Vector2Int(maxRank, maxRank-1);
                difficultyModifier = 2f;
            }

            bool bossWave = ++currentWave % bossEveryNthWave == 0;
            if (bossWave) Debug.LogWarning("BOSS enemy!!!");
            Debug.LogFormat("current wave {0}, enemy range: {1}", currentWave, waveRange);


            currentPauseLenght = pauseLenght;
            while (currentPauseLenght > 0)
            {
                string nextWaveText = bossWave ? string.Format("BOSS in {0}s", currentPauseLenght--) : string.Format("Wave {0} in {1}s", currentWave + 1, currentPauseLenght--);
                waveProgressText.text = nextWaveText;
                yield return new WaitForSeconds(1f);
            }


            makePopup(string.Format("WAVE {0} STARTING!", currentWave));
            yield return new WaitForSeconds(1.5f);
            removePopup();


            levelManager.levelDifficultyModifier *= 1.03f;
            if (!bossWave && OnWaveStartedCallback != null) OnWaveStartedCallback.Invoke(currentWave);

            

            currentWaveLenght = waveLenght;
            while (currentWaveLenght > 0)
            {
                waveProgressText.text = string.Format("Wave {0} end in {1}s", currentWave, currentWaveLenght--);
                yield return new WaitForSeconds(1f);
            }

            if (!bossWave && OnWaveEndedCallback != null) OnWaveEndedCallback.Invoke();


            makePopup(string.Format("WAVE {0} FINISHED!", currentWave));
            yield return new WaitForSeconds(1.5f);
            removePopup();
        }

    }

    private void makePopup(string message)
    {
        waveProgressText.text = "";
        waveDetailsPopup.SetActive(true);
        popupText.text = message;
    }

    private void removePopup()
    {
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
