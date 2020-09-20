using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup effects_group;
    public AudioMixerGroup music_group;
    public AudioMixer audioMixer;


    public Sound[] sound_effects;
    private Dictionary<string, int> _effects= new Dictionary<string, int>();
    
    public Sound[] music;
    private Dictionary<string, int> _music = new Dictionary<string, int>();

    public AudioSource currentlyPlayingMusic;

    #region AudioManagerSingleton
    public static AudioManager instance;
    void Awake()
    {
        

        if (instance != null)
        {
            Debug.Log("more than one instance of AudioManager found!");
        }
        instance = this;

        int i = 0;
        foreach (Sound s in sound_effects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.priority = 224;
            //s.source.spatialBlend = 0.8f;
            s.source.outputAudioMixerGroup = effects_group;

            _effects.Add(s.name, i++);
        }

        i = 0;
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = music_group;



            _music.Add(s.name, i++);
        }

    }
    #endregion

    private void Start()
    {
        


        bool a1 = audioMixer.SetFloat("master_volume", 27 * Mathf.Log10(PlayerPrefs.GetFloat("master_volume", 0.5f) + 0.001f));
        bool a2 = audioMixer.SetFloat("effects_volume", 27 * Mathf.Log10(PlayerPrefs.GetFloat("effects_volume", 0.5f) + 0.001f));
        bool a3 = audioMixer.SetFloat("music_volume", 27 * Mathf.Log10(PlayerPrefs.GetFloat("music_volume", 0.5f) + 0.001f));
    }

    public void PlayEffect(string sound_name)
    {
        if (!PlayerPrefs.GetFloat("effects_volume", 0.5f).Equals(0f) && _effects.ContainsKey(sound_name))
        {
            Sound s = sound_effects[_effects[sound_name]];
            s.source.Play();
        } else
        {
            Debug.LogWarning(sound_name + " doesn't exist");
        }
    }
    
    public void PlayMusic(string sound_name)
    {
        if (_music.ContainsKey(sound_name))
        {
            if (currentlyPlayingMusic != null) currentlyPlayingMusic.Stop();
            Sound s = music[_music[sound_name]];
            s.source.Play();
            currentlyPlayingMusic = s.source;
        } else
        {
            Debug.LogWarning(sound_name + " doesn't exist");
        }
    }

   


    




}
