using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region load scene methods

    public void LoadGameScene()
    {
        StopMenuMusic();
        SceneManager.LoadScene("Game");
    }
    public void LoadFinalScoreScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FinalScore");
    }
    public void LoadStartScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Destroy(FindObjectOfType<GameSessionScore>().gameObject);
        }
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void StopMenuMusic()
    {
        if (FindObjectOfType<MenuMusic>())
        {
            Destroy(FindObjectOfType<MenuMusic>().gameObject);
        }
    }

    public void LoadControlsScene()
    {
        SceneManager.LoadScene("Controls");
    }

    #endregion
}
