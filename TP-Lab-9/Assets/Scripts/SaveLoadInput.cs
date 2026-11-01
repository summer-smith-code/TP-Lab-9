using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadInput : MonoBehaviour
{
    // Save file name 
    public string saveID;

    // For binary score file
    public Score scoreManager;

    // For saving the Score Manager
    private void Start()
    {
        scoreManager = FindFirstObjectByType<Score>();

        if (scoreManager != null)
        {
            // Load score when the game starts
            scoreManager.LoadScore(saveID + "_score.dat");
        }
    }

    void Update()
    {
        // Save game when S is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }

        // Reload scene when L is pressed

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    private void SaveGame()
    {
        Debug.Log("Saving game...");

        SavingService.SaveGame(saveID);

        if (scoreManager != null)
        {
            scoreManager.SaveScore(saveID + "_score.dat");
        }

        Debug.Log("Game saved!");
    }

    private void LoadGame()
    {
        Debug.Log("Loading game...");

        SavingService.LoadGame(saveID);

        if (scoreManager != null)
        {
            scoreManager.LoadScore(saveID + "_score.dat");
        }

        Debug.Log("Game loaded!");
    }
}
