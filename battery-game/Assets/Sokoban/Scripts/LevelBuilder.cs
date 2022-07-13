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

[System.Serializable]
public class Tile
{
    public string character;
    public GameObject prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public List<Level> levels;
    public List<Tile> tiles;

    void Start() { ParseLevelsFromFile("Levels/Levels"); }

    void ParseLevelsFromFile(string fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(fileName);
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

    public void Build(int currentLevel)
    {
        Level level = levels[currentLevel];

        int x = -level.Width / 2;
        int y = -level.Height / 2;

        foreach (var row in level.rows)
        {
            foreach (var character in row)
            {
                GameObject prefab = GetPrefab(character);
                if (prefab)
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                x++;
            }
            y++;
            x = -level.Width / 2;
        }
    }

    GameObject GetPrefab(char character)
    {
        Tile tile = tiles.Find(tile => tile.character == character.ToString());
        return tile != null ? tile.prefab : null;
    }
}
