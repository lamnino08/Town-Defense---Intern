using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void SettingVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
        
    }
    public void SettingQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
        
    }
    public void SettingFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Change");
    }
    
    private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
}
