using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // For score saving
    private Score scoreManager;
    private SaveLoadInput saveLoadInput;

    void Start()
    {
        scoreManager = FindFirstObjectByType<Score>();
        saveLoadInput = FindFirstObjectByType<SaveLoadInput>();

        if (!SavingService.LoadGame("TPLab9"))
        {
            print("Hello");
            foreach(Row row in GameObject.FindObjectsByType<Row>(FindObjectsSortMode.None))
            {
                row.StartBuild(true);
            }
        }

        scoreManager.LoadScore(saveLoadInput.saveID + "_score.dat");
    }

    private void OnApplicationQuit()
    {
        SavingService.SaveGame("TPLab9");
        scoreManager.SaveScore(saveLoadInput.saveID + "_score.dat");
    }
}
