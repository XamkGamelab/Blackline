using UnityEngine;

namespace Scripts.DevConsole.Commands
{
    [CreateAssetMenu(fileName = "LogCommand", menuName = "DevConsole/Commands/LogCommand")]

    public class LogCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            string logtext = string.Join(" ", args); // Join the arguments into a single string with spaces
            Debug.Log(logtext); // Log the text to the Unity console

            return true; // Indicate that the command was processed successfully
        }
    }
}
