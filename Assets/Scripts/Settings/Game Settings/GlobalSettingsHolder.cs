using System;
using UnityEngine;

public class GlobalSettingsHolder : MonoBehaviour
{
    public static GlobalSettingsHolder Instance { get; private set; }

    [Header("Player Settings")]
    public PlayerSettingsData PlayerSettingsData;

    [Header("Game Settings")]
    [SerializeField]
    private GameDifficultySettings _difficultySettings;
    [SerializeField]
    private GameDifficultyDataSheet _currentDifficultyData;

    public GameDifficultyDataSheet CurrentDifficultyData => _currentDifficultyData;

    private void Awake() => Initialize();

    private void Initialize()
    {
        Instance = this;
        PlayerSettingsData = new PlayerSettingsData();
        _currentDifficultyData = _difficultySettings.Difficulties[0];
    }

    // When the settings are updated, call this method. //
    public void UpdatePlayerSettings(PlayerSettingsData newData) 
    {
        PlayerSettingsData = newData;
    }

    public void UpdateGameDifficulty(GameDifficultySettings newData)
    {
        _difficultySettings = newData;
    }

    // For reading & writing settings in JSON. //
    public string ToJson(object obj) => JsonUtility.ToJson(obj);
    public void FromJson(string json, Type dataType)
    {
        // Not sure how to pass in dataType as parameter. This below causes errors. -Shad //
        //dataType = JsonUtility.FromJson<dataType>(json);        
    }
}
