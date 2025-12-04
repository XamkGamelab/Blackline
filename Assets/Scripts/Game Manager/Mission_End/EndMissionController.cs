using UnityEngine;
using TMPro;

public class EndMissionController : MonoBehaviour
{
    [Header("References")]
    public GameObject endMissionCanvas;   // The UI Canvas (disabled by default)
    public TMP_Text timeText;             // TMP text slot for mission time
    public TMP_Text enemyText;            // TMP text slot for enemies killed    

    private float missionStartTime;
    private int enemiesKilled = 0;
    private bool missionEnded = false;

    void Start()
    {
        missionStartTime = Time.time;
        endMissionCanvas.SetActive(false); // Make sure UI is hidden on start
    }

    void Update()
    {
        if (!missionEnded && Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.Interact))
        {
            ShowEndMission();
        }
    }

    // Call this from your enemy scripts when something is killed
    public void RegisterEnemyKill()
    {
        enemiesKilled++;
    }

    private void ShowEndMission()
    {
        missionEnded = true;

        // Pause the game
        Time.timeScale = 0f;

        // Calculate mission duration
        float missionDuration = Time.time - missionStartTime;

        // Display values
        timeText.text = "" + FormatTime(missionDuration);
        enemyText.text = "" + enemiesKilled;

        // Show the UI
        endMissionCanvas.SetActive(true);
    }

    // Converts seconds MM:SS
    private string FormatTime(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    // Optional: call this from a button on the UI
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
