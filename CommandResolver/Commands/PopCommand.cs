using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class PopCommand : ICommand
    {
        public CommandContext Context { get; set; }

        /// <summary>
        /// Removes last variable from stack
        /// </summary>
        /// <param name="context">Program Context where result will bes stored</param>
        public PopCommand(CommandContext context)
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
                Context.PopLastStackElement();
            }
            catch (Exception)
            {

                throw new CommandExecutionException("There no element in stack to pop", Context.Stack);
            }
        }
    }
}
