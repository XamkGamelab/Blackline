using UnityEngine;
using TMPro; // Use TextMeshPro
using System.Collections;

public class KeybindButton : MonoBehaviour
{
    [Header("Setup")]
    public string keyName;          // Must match field name in PlayerSettingsData exactly (e.g. "JumpKey")
    public TMP_Text buttonLabel;    // Assign via Inspector

    private bool waitingForKey = false;

    private void Start()
    {
        // Delay a frame to make sure GlobalSettingsHolder.Instance is initialized
        StartCoroutine(InitializeWhenReady());
    }

    private IEnumerator InitializeWhenReady()
    {
        while (GlobalSettingsHolder.Instance == null || GlobalSettingsHolder.Instance.PlayerSettingsData == null)
            yield return null;

        UpdateLabel();
    }

    public void OnClick()
    {
        if (!waitingForKey)
            StartCoroutine(WaitForKey());
    }

    private IEnumerator WaitForKey()
    {
        waitingForKey = true;
        buttonLabel.text = "Press any key...";

        KeyCode newKey = KeyCode.None;

        while (newKey == KeyCode.None)
        {
            foreach (KeyCode kc in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kc))
                {
                    newKey = kc;
                    break;
                }
            }
            yield return null;
        }

        var data = GlobalSettingsHolder.Instance.PlayerSettingsData;
        var field = typeof(PlayerSettingsData).GetField(keyName);

        if (field != null)
        {
            field.SetValue(data, newKey);
            // Optionally rebuild the key map
            GlobalSettingsHolder.Instance.UpdatePlayerSettings(data);

            PlayerSettingsManager.SaveSettings(data);
        }
        else
        {
            Debug.LogWarning($"Field '{keyName}' not found in PlayerSettingsData!");
        }

        waitingForKey = false;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (buttonLabel == null)
        {
            Debug.LogWarning($"[{name}] ButtonLabel not assigned in the inspector.");
            return;
        }

        if (GlobalSettingsHolder.Instance == null)
        {
            buttonLabel.text = "Error: No GlobalSettingsHolder";
            return;
        }

        var data = GlobalSettingsHolder.Instance.PlayerSettingsData;
        var field = typeof(PlayerSettingsData).GetField(keyName);

        if (field != null)
        {
            KeyCode current = (KeyCode)field.GetValue(data);
            buttonLabel.text = $"{keyName}: {current}";
        }
        else
        {
            buttonLabel.text = $"{keyName}: (Invalid)";
        }
    }
}
