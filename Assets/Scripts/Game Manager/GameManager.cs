using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject pauseMenuUI;

    private GameState _gameState;

    private void Update()
    {
        if(Input.GetKeyDown(SettingsHolder.Data.PauseKey)) TogglePause();
    }

    // Hides the Pause Menu on start. Very clever Veeti. -Shad //
    private void Awake() => Initialize();

    private void Initialize()
    {
        pauseMenuUI.SetActive(false);
    }

    #region Pausing
    private bool CanTogglePause()
    {
        return _gameState == GameState.InGame || _gameState == GameState.Paused;
    }    

    public void TogglePause()
    {
        if (!CanTogglePause()) return;

        bool toggle = _gameState == GameState.InGame;
        GameState state = _gameState == GameState.InGame ? GameState.Paused : GameState.InGame; 

        SetGameState(state);

        // Set the boolean to its opposite value. Imagine like *-1. -Shad //
        pauseMenuUI.SetActive(toggle); // Feed the _paused boolean directly to the method. -Shad //
        Time.timeScale = toggle ? 0f : 1f; // Is _paused true? Then set to 0f, else set to 1f. -Shad //

        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; // Is _paused? Then free the cursor, else lock it. -Shad //
    }
    #endregion

    // Do not set _gameState directly, use this. -Shad //
    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
    }   
}

public enum GameState
{
    InGame,
    Paused,
    GameOver,
}
