using System.Linq;
using UnityEngine;
using System.Collections;
using Steamworks;

[System.Serializable]
public class Achievement
{
    public string ID = "ACHIEVEMENT_00";
    public string Name;
    [SerializeField] string Description;
    [SerializeField] Texture2D IconIncomplete, IconComplete;
    [SerializeField] float currentProgress = 0f, TargetProgress;
    public bool Earned = false;

    // Returns true if this progress added results in the Achievement being earned.
    public bool AddProgress(float progress)
    {
        if (Earned)
        {
            return false;
        }

        currentProgress += progress;
        if (currentProgress >= TargetProgress)
        {
            Earned = true;
            return true;
        }

        return false;
    }

    public void LockAchievement()
    {
        Earned = false;
        currentProgress = 0f;
        SteamUserStats.ClearAchievement(ID);
    }

    // Returns true if this progress set results in the Achievement being earned.
    public bool SetProgress(float progress)
    {
        if (Earned)
        {
            return false;
        }

        currentProgress = progress;
        if (progress >= TargetProgress)
        {
            Earned = true;
            return true;
        }

        return false;
    }
}
