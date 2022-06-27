using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class JSONSaving : MonoBehaviour
{
    private string path;

    void Start()
    {
        path = Application.streamingAssetsPath + "/" + "PlayerData.json";

        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);
    }

    public void SaveData(PlayerData playerData)
    {
        string statusString, batteryString, outputString = "{\"levels\":[";

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

    public PlayerData LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string[] input = reader.ReadToEnd().Replace("{\"levels\":[", "").Replace("],\"batteries\":[", ";").Replace("]}", "").Split(';');

        List<Status> statuses = new List<Status>();
        string[] statusesArray = input[0].Replace("\"", "").Split(',');
        foreach (string element in statusesArray)
            switch (element)
            {
                case "completed": statuses.Add(Status.completed); break;
                case "unlocked": statuses.Add(Status.unlocked); break;
                case "locked": statuses.Add(Status.locked); break;
                default: statuses.Add(Status.locked); break;
            }
        List<int> batteries = input[1].Replace("\"", "").Split(',').Select(Int32.Parse).ToList();

        return new PlayerData(statuses, batteries);
    }
}
