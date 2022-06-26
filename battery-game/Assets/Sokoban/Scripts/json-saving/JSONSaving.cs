using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class JSONSaving : MonoBehaviour
{
    private string path;

    void Start()
    {
        path = Application.streamingAssetsPath + "/" + "SaveData.json";

        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);
    }

    public void SaveList<Type>(List<Type> list, string fileName)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        string OutputString = "";
        foreach (var elem in list)
            OutputString += JsonUtility.ToJson(elem) + "\n";
        OutputString = OutputString.Substring(0, OutputString.Length - 1);
        Debug.Log(OutputString);
        System.IO.File.WriteAllText(path, OutputString);
    }

    public List<Type> LoadList<Type>(string fileName)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        string InputString = File.ReadAllText(path);

        List<string> InputStringList = InputString.Split('\n').ToList();
        List<Type> list = new List<Type>();
        foreach (var elem in InputStringList)
            list.Add(JsonUtility.FromJson<Type>(elem));
        return list;
    }

    public void SaveData(PlayerData playerData)
    {
        Debug.Log("Saving Data to " + path);
        using StreamWriter writer = new StreamWriter(path);
        string json = JsonUtility.ToJson(playerData);
        writer.Write(json);
        Debug.Log(json);

        // SaveList<Status>(playerData.GetStatuses(), "statuses.json");
        // SaveList<int>(playerData.GetBatteries(), "batteries.json");
    }

    public PlayerData LoadData()
    {
        Debug.Log("Loading Data from " + path);
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        Debug.Log(JsonUtility.FromJson<PlayerData>(json));
        return JsonUtility.FromJson<PlayerData>(json);

        // return new PlayerData(
        //     LoadList<Status>("statuses.json"),
        //     LoadList<int>("batteries.json")
        // );
    }
}
