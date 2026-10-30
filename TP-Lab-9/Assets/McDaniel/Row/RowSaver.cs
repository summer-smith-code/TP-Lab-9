using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class RowSaver : MonoBehaviour, ISaveable
{
    [SerializeField] Row row;

    private const string ENEMIES_KEY = "enemies";
    private const string LEFT_END_KEY = "leftEnd";
    private const string RIGHT_END_KEY = "rightEnd";
    private const string NEW_GAME_KEY = "newGame";

    public string SaveID { get; }
    public JsonData SavedData { 
        get {
            var result = new JsonData();
            result[ENEMIES_KEY] = SerializeValue(row.Targets);
            result[LEFT_END_KEY] = SerializeValue(row.endLPos);
            result[RIGHT_END_KEY] = SerializeValue(row.endRPos);
            result[NEW_GAME_KEY] = SerializeValue(row.newGame);
            return result;
        } 
    }

    void Awake()
    {
        if(row == null)
        {
            row = GetComponent<Row>();
        }
    }

    private JsonData SerializeValue(object obj)
    {
        return JsonMapper.ToObject(JsonUtility.ToJson(obj));
    }

    public void LoadFromData(JsonData data)
    {
        if (data.ContainsKey(ENEMIES_KEY))
            row.Targets = JsonMapper.ToObject<List<Vector3>>(data[ENEMIES_KEY].ToJson());

        if (data.ContainsKey(LEFT_END_KEY))
            row.endLPos = JsonMapper.ToObject<Vector3>(data[LEFT_END_KEY].ToJson());

        if (data.ContainsKey(RIGHT_END_KEY))
            row.endRPos = JsonMapper.ToObject<Vector3>(data[RIGHT_END_KEY].ToJson());

        if (data.ContainsKey(NEW_GAME_KEY))
            row.newGame = JsonMapper.ToObject<bool>(data[NEW_GAME_KEY].ToJson());

        row.StartBuild(row.newGame);
    }
}
