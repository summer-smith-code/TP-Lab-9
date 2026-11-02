using LitJson;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
public interface ISaveable
{
    // identifier for each saveable object
    string SaveID { get; }
    // the data to be saved for this object
    JsonData SavedData { get; }
    // load the data for this object from the given JsonData
    void LoadFromData(JsonData data);
}
