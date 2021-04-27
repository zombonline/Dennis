using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBox : MonoBehaviour
{
    GameSessionScore gameSession;

    [SerializeField] TextMeshProUGUI timeSurvivedText;
    [SerializeField] TextMeshProUGUI arrowsAvoidedText;
    [SerializeField] TextMeshProUGUI pointsPickedUpText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSessionScore>().GetComponent<GameSessionScore>();

        CheckHighScore();

        arrowsAvoidedText.text = "Arrows Avoided: " + gameSession.projectilesAvoided.ToString();
        pointsPickedUpText.text = "Points Picked up: " + gameSession.pointsPickedup.ToString();
        multiplierText.text = "Multiplier: " + gameSession.multiplier.ToString();
        finalScoreText.text = "Final Score: " + gameSession.CalculateFinalScore().ToString();
        highScoreText.text = "High Score: " + PlayerPrefsController.GetHighScore().ToString();
    }

    private void CheckHighScore()
    {
        if (gameSession.CalculateFinalScore() > PlayerPrefsController.GetHighScore())
        {
            PlayerPrefsController.SetHighScore(gameSession.CalculateFinalScore());
        }
    }
}
