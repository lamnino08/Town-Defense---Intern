using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip clickbutton;
    public AudioClip battestart;
    public AudioClip swordattack;
    public AudioClip bowattack;
    public AudioClip magicattack;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play() ;
    }
}
    