using UnityEngine;
using TMPro;

public static class HighScoreManager
{
    private const string TimeKey = "BestTime";

    // ✅ Save time (in total seconds)
    public static void SaveTime(float totalSeconds)
    {
        // If no previous save OR new time is better
        if (!PlayerPrefs.HasKey(TimeKey) || totalSeconds < PlayerPrefs.GetFloat(TimeKey))
        {
            PlayerPrefs.SetFloat(TimeKey, totalSeconds);
            PlayerPrefs.Save();
        }
    }

    // ✅ Load best time (returns -1 if none)
    public static float LoadTime()
    {
        if (PlayerPrefs.HasKey(TimeKey))
            return PlayerPrefs.GetFloat(TimeKey);

        return -1f;
    }

    // ✅ Format time for display
    public static string GetFormattedTime()
    {
        float time = LoadTime();

        if (time < 0)
            return "High Score: None";

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return $"Best Time    "+": {minutes}:{seconds:00}";
    }
}