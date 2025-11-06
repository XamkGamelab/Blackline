using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingsHolder : MonoBehaviour
{
    public static GlobalSettingsHolder Instance { get; private set; }

    [Header("Player Settings")]
    public PlayerSettingsData PlayerSettingsData = new PlayerSettingsData();

    [Header("Game Settings")]
    [SerializeField]
    private GameDifficultySettings _difficultySettings;
    [SerializeField]
    private GameDifficultyDataSheet _currentDifficultyData => _difficultySettings.Difficulties[0]; // Nasty fucking hack I fucking hate you Shad fuck you. -Shad //
    public GameDifficultyDataSheet CurrentDifficultyData => _currentDifficultyData;

    private Dictionary<KeyCode, WeaponCategory> _keyToCategory;
    public Dictionary<KeyCode, WeaponCategory> KeyToCategory => _keyToCategory;

    private void Awake() => Initialize();

    private void Initialize()
    {
        Instance = this;
        BuildKeybindMap();
    }

    // When the settings are updated, call this method. //
    public void UpdatePlayerSettings(PlayerSettingsData newData) 
    {
        PlayerSettingsData = newData;
        BuildKeybindMap();
    }

    public void UpdateGameDifficulty(GameDifficultySettings newData)
    {
        _difficultySettings = newData;
    }

    private void BuildKeybindMap()
    {
        _keyToCategory = new Dictionary<KeyCode, WeaponCategory>()
        {
            { PlayerSettingsData.MeleeKey, WeaponCategory.Melee },
            { PlayerSettingsData.LightCategoryKey, WeaponCategory.Light },
            { PlayerSettingsData.ShellCategoryKey, WeaponCategory.Shell },
            { PlayerSettingsData.MediumCategoryKey, WeaponCategory.Medium },
            { PlayerSettingsData.HeavyCategoryKey, WeaponCategory.Heavy },
            { PlayerSettingsData.PlasmaCategoryKey, WeaponCategory.Plasma },
            { PlayerSettingsData.RocketCategoryKey, WeaponCategory.Rocket },
            { PlayerSettingsData.UtilityCategoryKey, WeaponCategory.Utility },
            { PlayerSettingsData.ThrowableCategoryKey, WeaponCategory.Throwable },
        };
    }

    // Returns player settings back to default. -Shad //
    [ContextMenu("Reset Player Settings")]
    public void ResetPlayerSettings()
    {
        PlayerSettingsData playerSettingsData = new PlayerSettingsData();
        UpdatePlayerSettings(playerSettingsData);
    }

    // For reading & writing settings in JSON. //
    public string ToJson(object obj) => JsonUtility.ToJson(obj);
    public void FromJson(string json, Type dataType)
    {
        // Not sure how to pass in dataType as parameter. This below causes errors. -Shad //
        //dataType = JsonUtility.FromJson<dataType>(json);        
    }
}
