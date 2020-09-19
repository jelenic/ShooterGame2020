using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region AudioManagerSingleton
    public static AudioManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of AudioManager found!");
        }
        instance = this;


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }
    #endregion

    public Sound[] sounds;



}
