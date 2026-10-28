using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        TargetHit.OnHit += UpdateScore;
    }

    private void OnDisable()
    {
        TargetHit.OnHit -= UpdateScore;
    }

    internal void UpdateScore(int newScore)
    {
        score += newScore;
        Debug.Log("Score updated: " + score);
        if (scoreText == null)
        {
            scoreText = GetComponentInChildren<TextMeshProUGUI>();
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

  
}
