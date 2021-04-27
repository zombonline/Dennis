using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    GameSessionScore gameSession;


    private void Start()
    {
        gameSession =  FindObjectOfType<GameSessionScore>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + gameSession.CalculateFinalScore();
    }


}
