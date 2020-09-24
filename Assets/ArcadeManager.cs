using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    public bool arcadeActive;

    public int currentWave;

    public int waveLenght;
    public int pauseLenght;

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
    }

    private void startWaves()
    {
        StartCoroutine(arcadeLoop());
    }


    private IEnumerator arcadeLoop()
    {
        while (arcadeActive)
        {
            currentPauseLenght = pauseLenght;
            while (currentPauseLenght > 0)
            {
                waveProgressText.text = string.Format("Wave {0} in {1}s", currentWave + 1, currentPauseLenght--);
                yield return new WaitForSeconds(1f);
            }

            currentWave += 1;

            makePopup(string.Format("WAVE {0} STARTING!", currentWave));
            yield return new WaitForSeconds(1.5f);
            removePopup();

            levelManager.levelDifficultyModifier *= 1.03f;
            if (OnWaveStartedCallback != null) OnWaveStartedCallback.Invoke(currentWave);

            

            currentWaveLenght = waveLenght;
            while (currentWaveLenght > 0)
            {
                waveProgressText.text = string.Format("Wave {0} end in {1}s", currentWave, currentWaveLenght--);
                yield return new WaitForSeconds(1f);
            }

            if (OnWaveEndedCallback != null) OnWaveEndedCallback.Invoke();


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
