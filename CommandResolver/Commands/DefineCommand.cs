using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class DefineCommand : ICommand
    {
        public string VariableName { get; set; }
        public string Number { get; set; }
        public MutableKeyValuePair<string, object> Pair { get; set; }

        /// <summary>
        /// Setups Define command that defines your variable with given number
        /// </summary>
        /// <param name="variable">Your variable name</param>
        /// <param name="number">Your variable value</param>
        /// <param name="pair">MutableKeyPair where result will be stored</param>
        public DefineCommand(string variable, string number, ref MutableKeyValuePair<string, object> pair)
        {
            VariableName = variable ?? throw new CommandExecutionException("Variable name must be defined");
            Number = number ?? throw new CommandExecutionException("Variable must be defined");
            Pair = pair ?? throw new CommandExecutionException("Could process this if KeyValuePair isn't defined");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            if (Number.Contains('.'))
            {
                try
                {
                    Pair.Value = Convert.ToDouble(Number);
                }
                catch
                {
                    throw new CommandExecutionException("Wrong second parameter", Pair);
                }
            }
            else
            {
                try
                {
                    Pair.Value = Convert.ToInt64(Number);
                }
                catch
                {
                    throw new CommandExecutionException("Wrong second parameter", Pair);
                }
            }
            Pair.Id = VariableName;
        }
    }
}
