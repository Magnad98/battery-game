using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement
{
    public string character;
    public GameObject prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public List<LevelElement> levelElements;
    private Level level;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = levelElements.Find(le => le.character == c.ToString());
        return levelElement != null ? levelElement.prefab : null;
    }

    public void Build(int currentLevel)
    {
        level = GetComponent<Levels>().levels[currentLevel];

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
}
