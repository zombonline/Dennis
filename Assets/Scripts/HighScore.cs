using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI highScoreTextOutline;

    private void Start()
    {
        //if the player has set a high score display it in the main menu
        if(PlayerPrefsController.GetHighScore() >0)
        {
            highScoreText.text = "High Score: " + PlayerPrefsController.GetHighScore().ToString();
            highScoreTextOutline.text = "High Score: " + PlayerPrefsController.GetHighScore().ToString();
        }
        //if no high score found, disable the text in the main menu
        else
        {
            highScoreText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            highScoreTextOutline.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

}
