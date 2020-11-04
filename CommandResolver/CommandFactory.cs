using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommandResolver
{
    public class CommandFactory
    {
        public string Command { get; private set; }
        public string CommandFilePath { get; private set; } = @"Commands.cdat";
        public CommandContext Context { get; private set; }
        public Dictionary<string, string> CommandList { get; private set; }

        /// <summary>
        /// Creates factory for managing commands
        /// </summary>
        /// <param name="context">Content for managing</param>
        public CommandFactory(CommandContext context)
        {
            if (File.Exists(CommandFilePath))
            {
                string readText = File.ReadAllText(CommandFilePath);
                CommandList = new Dictionary<string, string>();
                Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");

                string[] linesFromFile = readText.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string commandLine in linesFromFile)
                {
                    if (commandLine != "")
                    {
                        string[] splitedCommands = commandLine.Split('\t');
                        CommandList.Add(splitedCommands[0], splitedCommands[1]);
                    }
                }
            }
            else
            {
                throw new Exception("Couldn't find file with commands");
            }
        }
        public ICommand GetCommand(string command)
        {
            string[] splitedCommands = command.Split(' ');
            string functionName = splitedCommands[0];
            List<object> invocationArgs = new List<object>();

            if (!CommandList.ContainsKey(functionName))
            {
                throw new CommandFactoryException($"There no such function: {functionName}");
            }

            var splitedCommandFromList = CommandList[functionName].Split(' ');

            functionName = splitedCommandFromList[0];
            for (int i = 1; i < splitedCommandFromList.Length; i++)
            {
                invocationArgs.Add(splitedCommandFromList[i]);
            }

            for (int i = 1; i < splitedCommands.Length; i++)
            {
                invocationArgs.Add(splitedCommands[i]);
            }


            string objectToInstantiate = $"CommandResolver.Commands.{functionName}, CommandResolver";

            Type objectType = Type.GetType(objectToInstantiate);

            invocationArgs.Add(Context);

            try
            {
                dynamic instantiatedObject = Activator.CreateInstance(objectType, invocationArgs.ToArray());
                return instantiatedObject;
            }
            catch
            {
                throw new CommandFactoryException($"Couldn't create object of \" {objectType.Name} \" instance, wrong function arguments");
            }
        }
    }
}
