using System.Linq;
using UnityEngine;
using System.Collections;
using Steamworks;
using System;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance = null;
    private bool unlockTest = false;

    public Achievement[] Achievements;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // deactivate this is no connection to steam
        if(!SteamManager.Initialized) 
        {
            gameObject.SetActive(false);
            return;
        }

    }

    private Achievement GetAchievementByName(string achievementName)
    {
        return Achievements.FirstOrDefault(achievement => achievement.Name == achievementName);
    }

    private void AchievementEarned(string achievementName)
    {
        Achievement achievement = GetAchievementByName(achievementName);

        // unlock steam achievement
        TestSteamAchievement(achievement.ID);
        if(!unlockTest)
        {
            SteamUserStats.SetAchievement(achievementName);
            SteamUserStats.StoreStats();
        }
    }

    // tests if achievement is locked
    private void TestSteamAchievement(string ID)
    {
        SteamUserStats.GetAchievement(ID, out unlockTest);
    }

    public void AddProgressToAchievement(string achievementName, float progressAmount)
    {
        Achievement achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.Log("AddProgressToAchievement() - Trying to add progress to an achievemnet that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.AddProgress(progressAmount))
        {
            AchievementEarned(achievementName);
            Debug.Log(achievementName + " achievement earned");
        }
    }

    public void SetProgressToAchievement(string achievementName, float newProgress)
    {
        Achievement achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.Log("SetProgressToAchievement() - Trying to add progress to an achievemnet that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.SetProgress(newProgress))
        {
            AchievementEarned(achievementName);
        }
    }
}
