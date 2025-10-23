using UnityEngine;

namespace Scripts.DevConsole.Commands 
    {
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand //Base class for console commands -Veeti//
    {
        [SerializeField] private string commandWord = string.Empty; //The command word for the console command -Veeti//
        public string CommandWord => commandWord; //Public getter for the command word -Veeti//
        public abstract bool Process(string[] args); //Abstract method for processing the command -Veeti//
    }
}
