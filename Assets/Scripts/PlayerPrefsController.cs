using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_MUSIC_VOLUME_KEY = "master music volume";
    const string MASTER_SFX_VOLUME_KEY = "master sfx volume";
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    const string HIGH_SCORE = "high score";

    public static void SetHighScore(float score)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE, score);
    }

    public static float GetHighScore()
    {
        return PlayerPrefs.GetFloat(HIGH_SCORE, 0);
    }
    public static void SetMasterMusicVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        PlayerPrefs.SetFloat(MASTER_MUSIC_VOLUME_KEY, volume);
        else
        {
            Debug.LogError("Volume out of range");
        }
    }
    public static void SetMasterSFXVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        PlayerPrefs.SetFloat(MASTER_SFX_VOLUME_KEY, volume);
        else
        {
            Debug.LogError("Volume out of range");
        }
    }
    public static float GetMasterMusicVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_MUSIC_VOLUME_KEY, 0.75f);
    }
    public static float GetMasterSFXVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_SFX_VOLUME_KEY, 1f);
    }

}
