using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    string path = $"{Application.streamingAssetsPath}/PlayerData.json";

    public string GetSaveGamePath() { return path; }

    void Start()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);
    }

    public PlayerData NewGame()
    {
        PlayerData playerData = new PlayerData(
                    0,
                    new List<Status>() { Status.unlocked, Status.locked, Status.locked, Status.locked, Status.locked, Status.locked, Status.locked },
                    new List<int>() { 0, 0, 0, 0, 0, 0 }
                );
        SaveGame(playerData);
        return playerData;
    }

    public PlayerData LoadGame()
    {
        using StreamReader reader = new StreamReader(path);
        string[] input = reader.ReadToEnd().Replace("{\"currentLevel\":", "").Replace(",\"levels\":[", ";").Replace("],\"batteries\":[", ";").Replace("]}", "").Split(';');

        int currentLevel = Int32.Parse(input[0]);

        List<Status> statuses = new List<Status>();
        string[] statusesArray = input[1].Replace("\"", "").Split(',');
        foreach (string element in statusesArray)
            switch (element)
            {
                case "completed": statuses.Add(Status.completed); break;
                case "unlocked": statuses.Add(Status.unlocked); break;
                case "locked": statuses.Add(Status.locked); break;
                default: statuses.Add(Status.locked); break;
            }
        List<int> batteries = input[2].Replace("\"", "").Split(',').Select(Int32.Parse).ToList();

        return new PlayerData(currentLevel, statuses, batteries);
    }

    public void SaveGame(PlayerData playerData)
    {
        string statusString, batteryString, outputString = "{\"currentLevel\":";

        outputString += playerData.GetCurrentLevel() + ",\"levels\":[";

        List<Status> statuses = playerData.GetStatuses();
        for (int i = 0; i < statuses.Count; i++)
        {
            statusString = $"\"{statuses[i]}\"";
            outputString += statusString;
            if (i < statuses.Count - 1)
                outputString += ",";
        }

        outputString += "],\"batteries\":[";
        List<int> batteries = playerData.GetBatteries();
        for (int i = 0; i < batteries.Count; i++)
        {
            batteryString = $"{batteries[i]}";
            outputString += batteryString;
            if (i < batteries.Count - 1)
                outputString += ",";
        }

        outputString += "]}";
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(outputString);
    }
}
