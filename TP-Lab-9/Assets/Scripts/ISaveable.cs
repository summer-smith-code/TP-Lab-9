using LitJson;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
public interface ISaveable
{
    string SaveID { get; }
    JsonData SavedData { get; }
    void LoadFromData(JsonData data);
}
