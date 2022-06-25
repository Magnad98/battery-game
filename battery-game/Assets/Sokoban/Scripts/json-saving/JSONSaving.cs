using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    private string path;

    void Start()
    {
        path = Application.streamingAssetsPath + "/" + "SaveData.json";

        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);
    }

    public void SaveData(PlayerData playerData)
    {
        // Debug.Log("Saving Data to " + path);
        using StreamWriter writer = new StreamWriter(path);
        string json = JsonUtility.ToJson(playerData);
        writer.Write(json);
        // Debug.Log(json);
    }

    public PlayerData LoadData()
    {
        // Debug.Log("Loading Data from " + path);
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        // Debug.Log(JsonUtility.FromJson<PlayerData>(json));
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
