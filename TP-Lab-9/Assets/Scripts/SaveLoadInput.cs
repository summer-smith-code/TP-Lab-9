using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadInput : MonoBehaviour
{
    // Save file name 
    [SerializeField] private String saveID;

    void Update()
    {
        // When player presses S, game is saved 
        if (Input.GetKey(KeyCode.S)) SavingService.SaveGame(saveID);

        // When player presses L, loads save by re-loading scene
        if (Input.GetKey(KeyCode.L)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
