using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level // A single level
{
    public List<string> m_Rows = new List<string>();

    public int Height { get { return m_Rows.Count; } }
    public int Width
    {
        //Width is length of longest row
        get
        {
            int maxLength = 0;
            foreach (var r in m_Rows)
            {
                if (r.Length > maxLength)
                {
                    maxLength = r.Length;
                }
            }
            return maxLength;
        }
    }
}

public class Levels : MonoBehaviour
{
    public string filename;
    public List<Level> m_Levels;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        if (!textAsset)
        {
            Debug.Log("Levels: " + filename + ".txt does not exist!");
            return;
        }
        else
        {
            Debug.Log("Levels imported");
        }
        string competeText = textAsset.text;
        string[] lines = competeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        m_Levels.Add(new Level());
        for (long i = 0; i < lines.LongLength; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("New level added");
                m_Levels.Add(new Level());
                continue;
            }
            m_Levels[m_Levels.Count - 1].m_Rows.Add(line); // Always adding level rows to least level in list of levels
        }
    }
}