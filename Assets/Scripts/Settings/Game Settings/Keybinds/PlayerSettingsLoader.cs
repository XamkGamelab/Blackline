using UnityEngine;

public class PlayerSettingsLoader : MonoBehaviour
{
    private void Start()
    {
        // Make sure GlobalSettingsHolder exists before this runs
        if (GlobalSettingsHolder.Instance != null)
        {
            var loadedData = PlayerSettingsManager.LoadSettings();
            GlobalSettingsHolder.Instance.UpdatePlayerSettings(loadedData);
        }
        else
        {
            Debug.LogWarning("GlobalSettingsHolder not found in scene when trying to load settings!");
        }
    }
}
