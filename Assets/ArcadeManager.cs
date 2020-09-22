using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    public int currentWave;

    public int waveLenght;
    public int pauseLenght;

    private int currentWaveLenght;
    private int currentPauseLenght;

    public GameObject waveDetailsPopup;
    public TextMeshProUGUI popupText;

    public TextMeshProUGUI waveProgressText;

    public delegate void VoidFunction();

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


        startPause();
    }

    private void startPause()
    {
        currentPauseLenght = pauseLenght;
        StartCoroutine(pauseTimer());
    }

    private IEnumerator pauseTimer()
    {
        while (currentPauseLenght > 0)
        {
            waveProgressText.text = string.Format("Wave {0} in {1}s", currentWave+1, currentPauseLenght--);
            yield return new WaitForSeconds(1f);
        }

        startNextWave();
    }

    private void startNextWave()
    {
        currentWave += 1;
        StartCoroutine(makePopup(string.Format("WAVE {0} STARTING!", currentWave), 1.5f, startWaveTimer));
    }
    private IEnumerator makePopup(string message, float duration, VoidFunction func)
    {
        waveProgressText.text = "";
        waveDetailsPopup.SetActive(true);
        popupText.text = message;

        yield return new WaitForSeconds(duration);

        popupText.text = "";
        waveDetailsPopup.SetActive(false);
        func();
    }

    private void startWaveTimer()
    {
        currentWaveLenght = waveLenght;
        StartCoroutine(waveTimer());
    }

    private IEnumerator waveTimer()
    {
        while (currentWaveLenght > 0)
        {
            waveProgressText.text = string.Format("Wave {0} end in {1}s", currentWave, currentWaveLenght--);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(makePopup(string.Format("WAVE {0} FINISHED!", currentWave), 1f, startPause));
    }

}
