using System.Collections.Generic;

public enum Status { locked, unlocked, completed }

public class PlayerData
{
    int currentLevel;
    List<Status> statuses;
    List<int> batteries;

    public int GetCurrentLevel() { return currentLevel; }

    public void SetCurrentLevel(int currentLevel) { this.currentLevel = currentLevel; }

    public List<Status> GetStatuses() { return statuses; }

    public List<int> GetBatteries() { return batteries; }

    public void CompleteCurrentLevel()
    {
        statuses[currentLevel] = Status.completed;
        if (currentLevel < 4 && statuses[currentLevel + 1] == Status.locked)
            statuses[currentLevel + 1] = Status.unlocked;
    }

    public PlayerData(int currentLevel, List<Status> statuses, List<int> batteries)
    {
        this.currentLevel = currentLevel;

        this.statuses = new List<Status>();
        foreach (Status status in statuses)
            this.statuses.Add(status);

        this.batteries = new List<int>();
        foreach (int battery in batteries)
            this.batteries.Add(battery);
    }

    public bool AddBatteries(int NineVolt, int D, int C, int AA, int AAA, int Cell)
    {
        batteries[0] += NineVolt;
        batteries[1] += D;
        batteries[2] += C;
        batteries[3] += AA;
        batteries[4] += AAA;
        batteries[5] += Cell;
        return CheckForUnlockedLevels();
    }

    bool CheckForUnlockedLevels()
    {
        bool unlocked = false;

        if (batteries[3] >= 3 && batteries[4] >= 2)
            unlocked = unlocked || UnlockLevel(6);
        if (batteries[0] + batteries[1] + batteries[2] + batteries[3] + batteries[4] + batteries[5] >= 10)
            unlocked = unlocked || UnlockLevel(7);

        return unlocked;
    }

    bool UnlockLevel(int levelID)
    {
        if (statuses[levelID - 1] == Status.locked)
        {
            statuses[levelID - 1] = Status.unlocked;
            return true;
        }
        return false;
    }
}
