using UnityEngine;

public class TargetSubject : MonoBehaviour
{

    public delegate void ScoreUpdated(int score);
    public event ScoreUpdated OnScoreUpdated;

    void Start()
    {
        AddScore();
    }

    public void AddScore()
    {
        OnScoreUpdated?.Invoke(1);
    }
}
