using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Level
{
    public List<string> rows = new List<string>();
    public int Height { get { return rows.Count; } }
    public int Width { get { return rows.Aggregate("", (longestRow, currentRow) => currentRow.Length > longestRow.Length ? currentRow : longestRow).Length; } }
}

public class Levels : MonoBehaviour
{
    public List<Level> levels;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Levels");
        if (!textAsset)
            return;

        string competeText = textAsset.text;
        string[] lines = competeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        levels.Add(new Level());
        for (long i = 0; i < lines.LongLength; i++)
        {
            if (lines[i].StartsWith(";"))
            {
                levels.Add(new Level());
                continue;
            }
            levels[levels.Count - 1].rows.Add(lines[i]);
        }
    }
}