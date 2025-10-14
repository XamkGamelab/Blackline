using UnityEngine;

public static class SettingsHolder
{
    public static SettingsData Data { get; private set; }

    // Initialize settings with default values. //
    static SettingsHolder()
    {
        Data = new SettingsData();
    }

    // When the settings are updated, call this method. //
    public static void Apply(SettingsData newData) 
    {
        Data = newData;
    }

    // For reading & writing settings in JSON. //
    public static string ToJson() => JsonUtility.ToJson(Data);
    public static void FromJson(string json) => Data = JsonUtility.FromJson<SettingsData>(json);
}
