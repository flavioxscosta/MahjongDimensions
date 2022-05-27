using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    //UI elements that control the master, music and sfx volumes (brightness not being used)
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider brightnessSlider;

    //Controller that handles the settings DTO which spans across scenes
    SettingsDTOController settingsDTO;

    //The main audio mixer
    public AudioMixer audioMixer;

    //Values of the master, music and sfx volume
    public float masterValue;
    public float musicValue;
    public float sfxValue;

    // Start is called before the first frame update
    void Start()
    {
        settingsDTO = GameObject.FindGameObjectWithTag("SettingsDTO").GetComponent<SettingsDTOController>();
        masterSlider.value = settingsDTO.masterValue;
        musicSlider.value = settingsDTO.musicValue;
        sfxSlider.value = settingsDTO.sfxValue;
        brightnessSlider.value = settingsDTO.brightnessValue;
        UpdateBrightness();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Changes the master volume according to the slider and updates it in the settingsDTO
    public void UpdateMasterVolume()
    {
        float volume = (float)Math.Floor(masterSlider.value) / 100f;
        if (volume == 0)
        {
            volume = 0.001f;
        }
        audioMixer.SetFloat("masterVolume", Mathf.Log(volume) * 20);
        masterValue = masterSlider.value;
        settingsDTO.masterValue = volume * 100;
    }

    //Changes the music volume according to the slider and updates it in the settingsDTO
    public void UpdateMusicVolume()
    {
        float volume = (float)Math.Floor(musicSlider.value) / 100f;
        if (volume == 0)
        {
            volume = 0.001f;
        }
        audioMixer.SetFloat("musicVolume", Mathf.Log(volume) * 20);
        musicValue = musicSlider.value;
        settingsDTO.musicValue = volume * 100; 
    }

    //Changes the sfx volume according to the slider and updates it in the settingsDTO
    public void UpdateSFXVolume()
    {
       float volume = (float)Math.Floor(sfxSlider.value) / 100f;
        if (volume == 0)
        {
            volume = 0.001f;
        }
        audioMixer.SetFloat("sfxVolume", Mathf.Log(volume) * 20);
        sfxValue = sfxSlider.value;
        settingsDTO.sfxValue = volume * 100;
    }

    //Changes the brightness according to the slider and updates it in the settingsDTO (not being used)
    public void UpdateBrightness()
    {
        float value = (float)Math.Floor(brightnessSlider.value) / 100f;
        settingsDTO.brightnessValue = value * 100;
        RenderSettings.ambientLight = new Color(value/5, value/5, value/5, 1.0f);
    }

}
