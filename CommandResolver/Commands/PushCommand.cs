using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;

namespace CommandResolver.Commands
{
    public class PushCommand : ICommand
    {
        public string VariableName { get; private set; }
        public string VariableValue { get; private set; }
        public CommandContext Context { get; set; }
        
        /// <summary>
        /// Pushes your defined variable to stack
        /// </summary>
        /// <param name="context">Program Context where result will bes stored</param>
        public PushCommand(CommandContext context)
        {
            Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");
        }

        /// <summary>
        /// Pushes your defined variable to stack
        /// </summary>
        /// <param name="variable">Your variable name</param>
        /// <param name="context">Program Context where result will bes stored</param>
        public PushCommand(string variable, CommandContext context)
        {
            VariableName = variable ?? throw new CommandExecutionException("Variable name must be defined");
            Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");
        }

        /// <summary>
        /// Pushes your defined variable to stack
        /// </summary>
        /// <param name="variable">Your variable name</param>
        /// <param name="value">Your variable value</param>
        /// <param name="context">Program Context where result will bes stored</param>
        public PushCommand(string variable, string value, CommandContext context)
        {
            VariableValue = value ?? throw new CommandExecutionException("Variable value must be defined");
            VariableName = variable ?? throw new CommandExecutionException("Variable name must be defined");
            Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            if (VariableName == null && VariableValue == null)
            {
                if (Context.Pair == null)
                    throw new CommandExecutionException($"You trying to push last defined valiable you didn't define nothing");
                Context.PushStack();
                return;
            }
            if (VariableValue == null && VariableValue != null)
            {
                if (Context.Pair.Id == null)
                    throw new CommandExecutionException($"You trying to push {VariableName} but this variable is not defined");

                if (Context.Pair.Id != VariableName)
                    throw new CommandExecutionException($"You trying to push {VariableName} but last variable that you defined is {Context.Pair.Id}", Context.Pair, Context.Stack);
                Context.PushStack(new MutableKeyValuePair<string, object>(Context.Pair.Id, Context.Pair.Value));
                return;
            }
            if (VariableName != null && VariableValue != null)
            {
                Context.PushStack(new MutableKeyValuePair<string, object>(VariableName, VariableValue));
                return;
            }

            throw new CommandExecutionException($"Nothing was pushed due to incorrect arguments", Context.Pair, Context.Stack);
        }
    }
}
