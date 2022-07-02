using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { locked, unlocked, completed }

public class PlayerData
{
    int currentLevel;
    List<Status> statuses;
    List<int> batteries;

    public PlayerData(int currentLevel, List<Status> statuses, List<int> batteries)
    {
        this.currentLevel = currentLevel;

        this.statuses = new List<Status>();
        foreach (Status status in statuses)
        {
            this.statuses.Add(status);
        }

        this.batteries = new List<int>();
        foreach (int battery in batteries)
        {
            this.batteries.Add(battery);
        }
    }

    public void AddBatteries(int NineVolt, int D, int C, int AA, int AAA, int Cell)
    {
        batteries[0] += NineVolt;
        batteries[1] += D;
        batteries[2] += C;
        batteries[3] += AA;
        batteries[4] += AAA;
        batteries[5] += Cell;
        CheckForUnlockedLevels();
    }

    void CheckForUnlockedLevels()
    {
        if (batteries[3] >= 3 && batteries[4] >= 2)
        {
            UnlockLevel(6);
        }
        if (batteries[0] + batteries[1] + batteries[2] + batteries[3] + batteries[4] + batteries[5] >= 10)
        {
            UnlockLevel(7);
        }
        // else
        // {
        //     Debug.Log("We're happy that you help us recycle! Keep it up! You will receive extra levels to play if you recycle more!");
        // }
    }

    void UnlockLevel(int levelID)
    {
        if (statuses[levelID - 1] == Status.locked)
        {
            statuses[levelID - 1] = Status.unlocked;
            Debug.Log($"Level {levelID} has been unlocked!");
        }
    }

    public void CompleteLevel(int levelID)
    {
        if (levelID > statuses.Count)
        {
            Debug.Log($"Level {levelID} can't be completed since it has not been unlocked!");
        }
        statuses[levelID - 1] = Status.completed;
        Debug.Log($"Level {levelID} has been completed!");
    }

    public override string ToString()
    {
        return $"\tStatuses: 1:{statuses[0]} 2:{statuses[1]} 3:{statuses[2]} 4:{statuses[3]} 5:{statuses[4]}, 6:{statuses[5]}, 7:{statuses[6]},\n\t\tBatteries: NnineVolt:{batteries[0]}, D:{batteries[1]}, C:{batteries[2]}, AA:{batteries[3]}, AAA:{batteries[4]}, Cell:{batteries[5]}";
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }

    public List<Status> GetStatuses()
    {
        return statuses;
    }

    public void CompleteCurrentLevel()
    {
        statuses[currentLevel] = Status.completed;
        if (currentLevel < 4 && statuses[currentLevel + 1] == Status.locked)
        {
            statuses[currentLevel + 1] = Status.unlocked;
        }
    }

    public List<int> GetBatteries()
    {
        return batteries;
    }
}
