using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] float volume;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterMusicVolume();
        LevelMusic();
    }

    public void setMusicVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }
    public void LevelMusic()
    {
        audioSource.Play();
    }

}
