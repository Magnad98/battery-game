using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    private string path;
    private PlayerData playerData;


    void Start()
    {
        CreateStreamingAssets(Application.streamingAssetsPath);
        path = Application.streamingAssetsPath + "/" + "SaveData.json";

        playerData = new PlayerData("Nico", 200f, 10f, 3);
    }

    void CreateStreamingAssets(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveData();

        if (Input.GetKeyDown(KeyCode.L))
            LoadData();
    }

    public void SaveData()
    {
        // Debug.Log("Saving Data to " + path);
        using StreamWriter writer = new StreamWriter(path);
        string json = JsonUtility.ToJson(playerData);
        writer.Write(json);
        // Debug.Log(json);
    }

    public void LoadData()
    {
        // Debug.Log("Loading Data from " + path);
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        playerData = new PlayerData(JsonUtility.FromJson<PlayerData>(json));
        // Debug.Log(playerData.ToString());
    }
}
