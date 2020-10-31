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
        public MutableKeyValuePair<string, object> MutablePairs { get; private set; }
        public Stack<MutableKeyValuePair<string, object>> MainStack { get; private set; }
        public Dictionary<string, string> CommandList { get; private set; }

        public CommandFactory(ref MutableKeyValuePair<string, object> pairs, ref Stack<MutableKeyValuePair<string, object>> stack)
        {
            if (File.Exists(CommandFilePath))
            {
                string readText = File.ReadAllText(CommandFilePath);
                CommandList = new Dictionary<string, string>();
                MutablePairs = pairs ?? throw new CommandExecutionException("Could process this if KeyValuePair isn't defined");
                MainStack = stack ?? throw new CommandExecutionException("Could process this if Stack isn't defined");

                string[] linesFromFile = readText.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
                );

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
            var splitedCommands = command.Split(' ');
            string functionName;

            List<object> invocationArgs = new List<object>();

            //1 more possible way to improve this switch!!!
            switch (splitedCommands.Length)
            {
                case 1:
                    functionName = splitedCommands[0];
                    break;
                case 2:
                    functionName = splitedCommands[0];
                    invocationArgs.Add(splitedCommands[1]);
                    break;
                case 3:
                    functionName = splitedCommands[0];
                    invocationArgs.Add(splitedCommands[1]);
                    invocationArgs.Add(splitedCommands[2]);
                    break;
                default:
                    throw new CommandFactoryException("Wrong number of parametrs");

            }
            if (CommandList.ContainsKey(functionName))
            {

                string objectToInstantiate = $"CommandResolver.Commands.{CommandList[functionName]}, CommandResolver";

                var objectType = Type.GetType(objectToInstantiate);

                invocationArgs.Add(MutablePairs);
                invocationArgs.Add(MainStack);

                try
                {
                    dynamic instantiatedObject = Activator.CreateInstance(objectType, invocationArgs.ToArray());
                    return instantiatedObject;
                }
                catch
                {

                    throw new CommandFactoryException($"Couldn't create objct of \" {objectType} \" instance");
                }
            }
            else
            {
                throw new CommandFactoryException("There no such command");
            }
        }
    }
}
