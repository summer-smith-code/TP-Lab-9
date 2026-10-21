using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    internal void UpdateScore(int newScore)
    {
        score += newScore;
        Debug.Log("Score updated: " + score);
    }
  
}
