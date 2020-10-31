﻿using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class PopCommand : ICommand
    {
        public Stack<MutableKeyValuePair<string, object>> MainStack { get; private set; }
        /// <summary>
        /// Removes last variable from stack
        /// </summary>
        /// <param name="stack">Main stack of programm where all varibles are stored</param>
        public PopCommand(MutableKeyValuePair<string, object> _, ref Stack<MutableKeyValuePair<string, object>> stack)
        {
            MainStack = stack ?? throw new CommandExecutionException("Could process this if Stack isn't defined");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            if (MainStack.TryPop(out MutableKeyValuePair<string, object> result))
            {
                Console.WriteLine($"Element with name: {result.Id} and value: {result.Value} was removed from stack");
            }
            else
                throw new CommandExecutionException("There no element in stack to pop", MainStack);
        }
    }
}