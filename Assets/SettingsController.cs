using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public GameObject hidingValue;
    private TextMeshProUGUI hidingText;
    public GameObject hpTimerSlider;
    private Slider timerSlider;

    public GameObject effectsVolumeSlider;
    private Slider effectsSlider;

    public GameObject musicVolumeSlider;
    private Slider musicSlider;

    [Range(0f, 15f)]
    public float maxTimer;

    private void Awake()
    {
        hidingText = hidingValue.GetComponent<TextMeshProUGUI>();
        timerSlider = hpTimerSlider.GetComponent<Slider>();
        timerSlider.onValueChanged.AddListener(delegate { onTimerChangedCallback(); });
        timerSlider.value = PlayerPrefs.GetFloat("hp_timer", 3f) / maxTimer;
        onTimerChangedCallback();

        effectsSlider = effectsVolumeSlider.GetComponent<Slider>();
        effectsSlider.value = PlayerPrefs.GetFloat("effects_volume", 0.5f);

        musicSlider = musicVolumeSlider.GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat("music_volume", 0.5f);
    }

    private void onTimerChangedCallback()
    {
        float value = timerSlider.value;
        float actualValue = value * maxTimer;
        string text = null;

        if (value.Equals(0f)) text = "Don't show";
        else if (value.Equals(1f)) text = "Don't hide";
        else text = string.Format("{0}s", actualValue.ToString("F1"));

        hidingText.text = text;
    }

    public void Back()
    {
        PlayerPrefs.SetFloat("effects_volume", effectsSlider.value);
        PlayerPrefs.SetFloat("music_volume", musicSlider.value);
        PlayerPrefs.SetFloat("hp_timer", timerSlider.value * maxTimer);
    }
}
