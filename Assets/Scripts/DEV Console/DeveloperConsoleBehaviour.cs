using Scripts.DevConsole.Commands;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


namespace Scripts.DevConsole
{
    public class DeveloperConsoleBehaviour : MonoBehaviour //MonoBehaviour for the developer console -Veeti//
    {
        [SerializeField] private string prefix = string.Empty; // Prefix for the console commands -Veeti//
        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];  // Array of console commands -Veeti//

        [Header("UI")] // UI elements for the console -Veeti//
        [SerializeField] private GameObject consoleUI = null; // Console UI GameObject -Veeti//
        [SerializeField] private TMP_InputField inputField = null; // Input field for the console -Veeti//

        private float pausedTimeScale; // Variable to store the time scale when the console is opened -Veeti//

        private static DeveloperConsoleBehaviour instance; // Singleton instance of the DeveloperConsoleBehaviour -Veeti//

        private DeveloperConsole developerConsole; // Instance of the DeveloperConsole -Veeti//

        private DeveloperConsole DeveloperConsole // Property to access the DeveloperConsole instance -Veeti//
        {
            get
            {
                if (developerConsole != null) { return developerConsole; } // If already initialized, return it -Veeti//
                return developerConsole = new DeveloperConsole(prefix, commands); // Otherwise, create a new instance -Veeti//
            }
        }

        private void Awake() // Awake method to initialize the singleton instance -Veeti//
        {
            if (instance != null && instance != this) // Check if an instance already exists -Veeti//
            {
                Destroy(gameObject); // If it does, destroy GameObject -Veeti//
                return;
            }
            instance = this; // Set the instance to this -Veeti//

            DontDestroyOnLoad(gameObject); // Make GameObject persistent across scenes -Veeti//
        }

        public void Toggle(CallbackContext context)
        {
            if (!context.action.triggered) { return; } // Only toggle on action triggered -Veeti//

            if (consoleUI.activeSelf) // If console is currently active -Veeti//
            {
                consoleUI.SetActive(false); // Deactivate console UI -Veeti//
                Time.timeScale = pausedTimeScale; // Restore the previous time scale -Veeti//
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor -Veeti//
            }
            else // If console is currently inactive -Veeti//
            {
                pausedTimeScale = Time.timeScale; // Store the current time scale -Veeti//
                consoleUI.SetActive(true); // Activate console UI -Veeti//
                Time.timeScale = 0f; // Pause the game by setting time scale to 0 -Veeti//
                inputField.ActivateInputField(); // Focus on the input field -Veeti//
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor -Veeti//
            }
        }

        public void ProcessCommand(string inputValue)
        {
            DeveloperConsole.ProcessCommand(inputValue); // Process the command input -Veeti//

            inputField.text = string.Empty; // Clear the input field -Veeti//
        }

    }
}

