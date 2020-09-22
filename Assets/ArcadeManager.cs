using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    public int currentWave;

    public float waveLenght;

    public GameObject waveDetailsPopup;
    public TextMeshProUGUI popupText;

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


        startNextWave();
    }

    private void startNextWave()
    {
        currentWave += 1;
        waveDetailsPopup.SetActive(true);
        popupText.text = string.Format("WAVE {0} STARTING!", currentWave);
        StartCoroutine(disablePopup(3f));
    }

    private IEnumerator disablePopup(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        popupText.text = "";
        waveDetailsPopup.SetActive(false);
    }
}
