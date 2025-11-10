using System.IO;
using UnityEngine;

public static class PlayerSettingsManager
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "playersettings.json");

    public static void SaveSettings(PlayerSettingsData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Saved player settings to {SavePath}");
    }

    public static PlayerSettingsData LoadSettings()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerSettingsData>(json);
        }

        // Return default if no file exists yet
        var newData = new PlayerSettingsData();
        SaveSettings(newData); // optionally write defaults
        return newData;
    }
}
