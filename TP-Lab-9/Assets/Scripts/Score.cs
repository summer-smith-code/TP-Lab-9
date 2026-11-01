using System.IO;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI scoreText;

    // Update score UI when game starts
    private void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateScoreUI();
    }

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

        UpdateScoreUI();
    }

    // Update score text whenever score should change
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Save score to binary
    public void SaveScore(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(score);
        }

        Debug.Log("Score saved: " + score);
    }

    // Load score from binary
    public void LoadScore(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            Debug.Log("No score save file found.");
            score = 0;
        }
        else
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                score = reader.ReadInt32();
            }

            Debug.Log("Score loaded: " + score);
        }

        UpdateScoreUI();
    }
}
