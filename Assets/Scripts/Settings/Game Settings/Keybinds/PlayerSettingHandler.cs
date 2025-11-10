using UnityEngine;

public class PlayerSettingsHandler : MonoBehaviour
{
    public static PlayerSettingsHandler Instance;
    public PlayerSettingsData Data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Data = PlayerSettingsManager.LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        PlayerSettingsManager.SaveSettings(Data);
    }
}
