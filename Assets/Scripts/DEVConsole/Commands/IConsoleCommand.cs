namespace Scripts.DevConsole.Commands 
{
    public interface IConsoleCommand //Interface for console commands -Veeti//
    {
        string CommandWord { get; } //Property for the command word -Veeti//
        bool Process(string[] args); //Method for processing the command -Veeti//
    }
}