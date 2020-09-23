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

            currentWaveLenght = waveLenght;
            while (currentWaveLenght > 0)
            {
                waveProgressText.text = string.Format("Wave {0} end in {1}s", currentWave, currentWaveLenght--);
                yield return new WaitForSeconds(1f);
            }

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
