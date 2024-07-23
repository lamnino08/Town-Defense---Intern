using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssitance : MonoBehaviour
{
    public static AudioAssitance Instance;
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    public AudioSource musicSoure, sfxSoure;
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Playmusic("Theme");
    }

    public void Playmusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        musicSoure.clip = s.clip;
        musicSoure.Play();
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        sfxSoure.PlayOneShot(s.clip);
    }
    public AudioClip GetClipByName(string name)
    {
        return Array.Find(sfxSounds, x => x.name == name).clip;
    }
    

}
