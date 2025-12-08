using UnityEngine;
using TMPro;

public class EndMissionController : MonoBehaviour
{
    [Header("References")]
    public GameObject endMissionCanvas;   // The UI Canvas (disabled by default)
    public TMP_Text timeText;             // TMP text slot for mission time
    public TMP_Text enemyText;            // TMP text slot for enemies killed

    [Header("Interaction (Look-and-Press)")]
    [Tooltip("Camera used for the look ray. If null, Camera.main will be used.")]
    public Camera playerCamera;
    [Tooltip("Maximum distance (meters) the player can be from this object to interact.")]
    public float interactDistance = 3f;

    private float missionStartTime;
    private int enemiesKilled = 0;
    private bool missionEnded = false;

    // Optional: public read-only to show in inspector during development
    [HideInInspector] public bool playerIsLooking = false;

    void Start()
    {
        missionStartTime = Time.time;
        if (endMissionCanvas != null)
            endMissionCanvas.SetActive(false); // Make sure UI is hidden on start
    }

    void Update()
    {
        if (missionEnded)
            return;

        Camera cam = playerCamera != null ? playerCamera : Camera.main;
        if (cam == null)
        {
            Debug.LogWarning($"[{name}] No camera assigned and Camera.main is null. Assign a Camera to 'playerCamera' or tag your main camera correctly.");
            return;
        }

        // Raycast from camera forward to check if the player is looking at this object (or one of its children)
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Allow interaction if the hit transform is this object or a child of it
            if (hit.transform.IsChildOf(transform))
            {
                playerIsLooking = true;

                // Only allow ending mission when player is looking at this object AND presses the interact key
                if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.Interact))
                {
                    ShowEndMission();
                }

                return;
            }
        }

        playerIsLooking = false;
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
        if (timeText != null)
            timeText.text = FormatTime(missionDuration);
        if (enemyText != null)
            enemyText.text = enemiesKilled.ToString();

        // Show the UI
        if (endMissionCanvas != null)
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
