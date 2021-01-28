using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    
    private void Awake()
    {
        SingletonPattern();
    }

    private void SingletonPattern()
    {
        if ( FindObjectsOfType(GetType()).Length > 1 )
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
   public int GetScore()
    {
        return score;
    }

    public void AddToScore(int given_score)
    {
        score += given_score;
    }
 
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
