using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssitance : MonoBehaviour
{
    public static AudioAssitance Instance {get; private set;}
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    public AudioSource musicSoure, sfxSoure;
    [SerializeField]
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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
        musicSoure.loop = true;
        musicSoure.Play();
    }

    
    public void AttackSoud()
    {
        StartCoroutine(PlayAttackSoundAfterDelay(2f));
    }

    private IEnumerator PlayAttackSoundAfterDelay(float delay)
    {
        // Lấy âm thanh "AttackSound" từ mảng
        Sound s = Array.Find(sfxSounds, x => x.name == "AttackSound");

        if (s != null)
        {
            // Đặt clip và phát âm thanh
            sfxSoure.clip = s.clip;
            sfxSoure.Play();

            // Chờ một khoảng thời gian trước khi in thông báo ra Debug
            yield return new WaitForSeconds(delay);

            Playmusic("Battle");
        }
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
