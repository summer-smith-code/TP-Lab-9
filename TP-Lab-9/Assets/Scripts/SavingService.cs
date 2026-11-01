using LitJson;
using System.IO;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SavingService
{
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    public static void SaveGame(string fileName) {
        //Saving Enemies
        var result = new JsonData();
        var allSaveableObjects = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
        if (allSaveableObjects.Count() > 0)
        {
            var savedObjects = new JsonData();
            foreach (var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;
                if (data.IsObject)
                {
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    savedObjects.Add(data);
                }
                else
                {
                    var behaviour = saveableObject as MonoBehaviour;
                    Debug.LogWarningFormat(behaviour, "{0}'s save data is not a dictionary. The object was not saved.",
                        behaviour.name);
                }
            }
            result[OBJECTS_KEY] = savedObjects;
        }
        else
        {
            Debug.LogWarningFormat("The scene did not include any saveable objects.");
        }

        var outputPath = Path.Combine(Application.persistentDataPath, fileName);
        var writer = new JsonWriter();
        writer.PrettyPrint = true;
        result.ToJson(writer);
        File.WriteAllText(outputPath, writer.ToString());
        Debug.LogFormat("Wrote saved game to {0}", outputPath);

        result = null;
        System.GC.Collect();
    }

    public static bool LoadGame(string fileName) 
    {
        //Loading Enemies
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(dataPath) == false) 
        { 
            Debug.LogErrorFormat("No file exists at {0}", dataPath); 
            return false; 
        }

        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);
        if (data == null || data.IsObject == false) 
        { 
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath);
            return false; 
        }

        if (data.ContainsKey(OBJECTS_KEY))
        {
            var objects = data[OBJECTS_KEY];
            
            var allLoadableObjects = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o);
            var objectsCount = objects.Count;
            for (int i = 0; i < objectsCount; i++)
            {
                var objectData = objects[i];
                var saveID = (string)objectData[SAVEID_KEY];
                if (allLoadableObjects.ContainsKey(saveID))
                {
                    var loadableObject = allLoadableObjects[saveID];
                    loadableObject.LoadFromData(objectData);
                }
            }
                System.GC.Collect();
        }

        return true;
    }
}
