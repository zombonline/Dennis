using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] float volume;
    AudioSource audioSource;

    //keep game objects between scenes unless deleted
    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<MenuMusic>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterMusicVolume();
        PlayMenuMusic();
    }

    public void setMusicVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }
    public void PlayMenuMusic()
    {
        audioSource.Play();
    }

}
