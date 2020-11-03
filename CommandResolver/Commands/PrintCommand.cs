using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class PrintCommand : ICommand
    {
        public CommandContext Context { get; set; }
        /// <summary>
        /// Prints last vaariable from stack
        /// </summary>
        /// <param name="context">Program Context where result will bes stored</param>
        public PrintCommand(CommandContext context)
        {
            Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");
        }
        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            try
            {
                Console.WriteLine($"Last element in stack has name: {Context.PeekLastStackElement().Id} and value: {Context.PeekLastStackElement().Value}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
