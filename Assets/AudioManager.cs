using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sound_effects;
    private Dictionary<string, int> _effects= new Dictionary<string, int>();
    
    public Sound[] music;
    private Dictionary<string, int> _music = new Dictionary<string, int>();

    private AudioSource currentlyPlayingMusic;

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
            s.source.spatialBlend = 0.8f;

            _effects.Add(s.name, i++);
        }

        i = 0;
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * PlayerPrefs.GetFloat("music_volume", 0.5f);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;


            _music.Add(s.name, i++);
        }

    }
    #endregion

    public void PlayEffect(string sound_name)
    {
        if (_effects.ContainsKey(sound_name))
        {
            Sound s = sound_effects[_effects[sound_name]];
            s.source.PlayOneShot(s.source.clip, PlayerPrefs.GetFloat("effects_volume", 0.5f));
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
