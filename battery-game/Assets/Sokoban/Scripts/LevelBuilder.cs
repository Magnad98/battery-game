using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement // Defines each item in a level by mapping a single character (i.e.: #) to a prefab
{
    public string m_Character;
    public GameObject m_Prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public List<LevelElement> m_LevelElements;
    private Level m_Level;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
        {
            return levelElement.m_Prefab;
        }
        else
        {
            return null;
        }
    }

    public void Build(int currentLevel)
    {
        m_Level = GetComponent<Levels>().m_Levels[currentLevel];
        // Offset coordinates so that centre of level is roughly at (0,0)
        int startX = -m_Level.Width / 2; // Save start x since it needs to be reset in loop
        int x = startX;
        int y = -m_Level.Height / 2;
        foreach (var row in m_Level.m_Rows)
        {
            foreach (var character in row)
            {
                GameObject prefab = GetPrefab(character);
                if (prefab)
                {
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startX;
        }
    }
}
