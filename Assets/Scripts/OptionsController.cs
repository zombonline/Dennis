using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sFXVolumeSlider;

    private void Awake()
    {
        //set slider values to current volumes upon loading scene
        musicVolumeSlider.value = PlayerPrefsController.GetMasterMusicVolume();
        sFXVolumeSlider.value = PlayerPrefsController.GetMasterSFXVolume();
    }

    private void Update()
    {
        //change music & sfx volume to whatever slider is changed to
        var musicPlayer = FindObjectOfType<Music>();
        if(musicPlayer)
        {
            musicPlayer.setMusicVolume(musicVolumeSlider.value);
        }
        else
        {
            Debug.LogError("No Music Player found!");
        }
        
        var menuMusicPlayer = FindObjectOfType<MenuMusic>();
        if (menuMusicPlayer)
        {
            menuMusicPlayer.setMusicVolume(musicVolumeSlider.value);
        }
        else
        {
            Debug.Log("No Menu Music Player found!");
            return;
        }
    }
    public void SaveSettings()
    {
        PlayerPrefsController.SetMasterMusicVolume(musicVolumeSlider.value);
        PlayerPrefsController.SetMasterSFXVolume(sFXVolumeSlider.value);
    }

}

