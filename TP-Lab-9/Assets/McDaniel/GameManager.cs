using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        if(!SavingService.LoadGame("TPLab9"))
        {
            print("Hello");
            foreach(Row row in GameObject.FindObjectsByType<Row>(FindObjectsSortMode.None))
            {
                row.StartBuild(true);
            }
        }
    }

    private void OnApplicationQuit()
    {
        SavingService.SaveGame("TPLab9");
    }
}
