using Scripts.DevConsole.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.DevConsole //DONT TOUCH ANY OF THIS -Veeti//
{
    public class DeveloperConsole : MonoBehaviour //Main class for the developer console -Veeti//
    {
        private readonly string prefix;
        private readonly IEnumerable<IConsoleCommand> commands;

        public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands) //Constructor for the developer console -Veeti//
        {
            this.prefix = prefix; //Set the prefix for the console commands -Veeti//
            this.commands = commands; //Set the commands for the console -Veeti//
        }

        public void ProcessCommand(string inputValue)
        {
            if (!inputValue.StartsWith(prefix)) { return; } //Check if the input value starts with the prefix -Veeti//

            inputValue = inputValue.Remove(0, prefix.Length); //Remove the prefix from the input value -Veeti//

            string[] inputSplit = inputValue.Split(' '); //Split the input value into command and arguments -Veeti//

            string commandInput = inputSplit[0]; //Get the command input -Veeti//
            string[] args = inputSplit.Skip(1).ToArray(); //Get the arguments -Veeti//

            ProcessCommand(commandInput, args); //Process the command input and arguments -Veeti//
        }

        public void ProcessCommand(string commandInput, string[] args) //Process the command input and arguments -Veeti//
        {
            foreach (var command in commands)
            {
                if (!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase)) //Check if the command input matches the command word -Veeti//
                {
                    continue;
                }
                if (command.Process(args)) //Process the command and check if it was successful -Veeti//
                {
                    return;
                }
            }

        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExtensionOfNativeClassAttribute : Attribute
    {
    }
}
