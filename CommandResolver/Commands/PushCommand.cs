using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class PushCommand : ICommand
    {
        public string VariableName { get; private set; }
        public Stack<MutableKeyValuePair<string, object>> MainStack { get; private set; }
        public MutableKeyValuePair<string, object> Pair { get; private set; }

        /// <summary>
        /// Pushes your defined variable to stack
        /// </summary>
        /// <param name="variable">Your variable name</param>
        /// <param name="pair">MutableKeyPair where result will be stored</param>
        /// <param name="stack">Main stack of programm where all varibles are stored</param>
        public PushCommand(string variable, ref MutableKeyValuePair<string, object> pair, ref Stack<MutableKeyValuePair<string, object>> stack)
        {
            VariableName = variable ?? throw new CommandExecutionException("Variable name must be defined");
            MainStack = stack ?? throw new CommandExecutionException("Could process this if Stack isn't defined");
            Pair = pair ?? throw new CommandExecutionException("Could process this if KeyValuePair isn't defined");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            if (Pair.Id == null)
                throw new CommandExecutionException($"You trying to push {VariableName} but this variable is not defined", Pair, MainStack);

            if (Pair.Id != VariableName)
                throw new CommandExecutionException($"You trying to push {VariableName} but last variable that you defined is {Pair.Id}", Pair, MainStack);

            MainStack.Push(new MutableKeyValuePair<string, object>(Pair.Id, Pair.Value));
        }
    }
}
