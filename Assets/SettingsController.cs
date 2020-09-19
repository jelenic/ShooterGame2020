using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public GameObject hidingValue;
    private TextMeshProUGUI hidingText;
    public GameObject hpTimerSlider;
    private Slider timerSlider;

    public GameObject masterVolumeSlider;
    private Slider masterSlider;

    public GameObject effectsVolumeSlider;
    private Slider effectsSlider;

    public GameObject musicVolumeSlider;
    private Slider musicSlider;

    public AudioMixer audioMixer;

    [Range(0f, 15f)]
    public float maxTimer;

    private void Awake()
    {
        hidingText = hidingValue.GetComponent<TextMeshProUGUI>();
        timerSlider = hpTimerSlider.GetComponent<Slider>();
        timerSlider.onValueChanged.AddListener(delegate { onTimerChangedCallback(); });
        timerSlider.value = PlayerPrefs.GetFloat("hp_timer", 3f) / maxTimer;
        onTimerChangedCallback();

        masterSlider = masterVolumeSlider.GetComponent<Slider>();
        masterSlider.onValueChanged.AddListener(delegate { onMasterVolumeChangedCallback(); });
        masterSlider.value = PlayerPrefs.GetFloat("master_volume", 0.5f);
        
        effectsSlider = effectsVolumeSlider.GetComponent<Slider>();
        effectsSlider.value = PlayerPrefs.GetFloat("effects_volume", 0.5f);

        musicSlider = musicVolumeSlider.GetComponent<Slider>();
        musicSlider.onValueChanged.AddListener(delegate { onMusicVolumeChangedCallback(); });
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

    private void onMasterVolumeChangedCallback()
    {
        bool a = audioMixer.SetFloat("master_volume", 27 * Mathf.Log10(masterSlider.value + 0.001f));
    }
    private void onMusicVolumeChangedCallback()
    {
        bool a = audioMixer.SetFloat("music_volume", 27 * Mathf.Log10(musicSlider.value + 0.001f));

    }


    public void Back()
    {
        PlayerPrefs.SetFloat("master_volume", masterSlider.value);
        PlayerPrefs.SetFloat("effects_volume", effectsSlider.value);
        PlayerPrefs.SetFloat("music_volume", musicSlider.value);
        PlayerPrefs.SetFloat("hp_timer", timerSlider.value * maxTimer);

        bool a = audioMixer.SetFloat("effects_volume", 27 * Mathf.Log10(effectsSlider.value + 0.001f));

        
    }
}
