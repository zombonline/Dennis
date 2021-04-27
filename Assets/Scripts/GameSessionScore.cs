using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionScore : MonoBehaviour
{
    public int projectilesAvoided;
    public int pointsPickedup;
    public int multiplier = 1;
    public float finalScore;

    //keep object through scenes unless deleted
    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSessionScore>().Length;
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


    public int CalculatePickupScore()
    {
        var finalPickupScore = pointsPickedup * multiplier;
        return finalPickupScore;
    }

    public float CalculateFinalScore()
    {
        var finalScore = (pointsPickedup + projectilesAvoided) * multiplier;
        return finalScore;
    }


}
