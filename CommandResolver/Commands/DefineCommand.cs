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
        public CommandContext Context { get; set; }

        /// <summary>
        /// Setups Define command that defines your variable with given number
        /// </summary>
        /// <param name="variable">Your variable name</param>
        /// <param name="number">Your variable value</param>
        /// <param name="context">Program Context where result will bes stored</param>
        public DefineCommand(string variable, string number, CommandContext context)
        {
            VariableName = variable ?? throw new CommandExecutionException("Variable name must be defined");
            Number = number ?? throw new CommandExecutionException("Variable must be defined");
            Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            object value;

            if (Number.Contains('.'))
            {
                try
                {
                    value = Convert.ToDouble(Number);
                }
                catch
                {
                    throw new CommandExecutionException("Wrong second parameter", Context.Pair);
                }
            }
            else
            {
                try
                {
                    value = Convert.ToInt64(Number);
                }
                catch
                {
                    throw new CommandExecutionException("Wrong second parameter", Context.Pair);
                }
            }
            Context.AddVariable(VariableName, value);
        }
    }
}
