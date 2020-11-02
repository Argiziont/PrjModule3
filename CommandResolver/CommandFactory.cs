using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            string functionName= splitedCommands[0];

            List<object> invocationArgs = new List<object>();

            for (int i = 1; i < splitedCommands.Length; i++)
            {
                invocationArgs.Add(splitedCommands[i]);
            }

            if (CommandList.ContainsKey(functionName))
            {

                string objectToInstantiate = $"CommandResolver.Commands.{CommandList[functionName]}, CommandResolver";

                var objectType = Type.GetType(objectToInstantiate);

                var paramTypes = getAllCtorsParamTypes(objectType);

                if(paramTypes.SingleOrDefault(p => p.Contains("MutableKeyValuePair"))!=null)
                    invocationArgs.Add(MutablePairs);

                if (paramTypes.SingleOrDefault(p => p.Contains("Stack")) != null)
                    invocationArgs.Add(MainStack);

                try
                {
                    dynamic instantiatedObject = Activator.CreateInstance(objectType, invocationArgs.ToArray());
                    return instantiatedObject;
                }
                catch
                {
                    throw new CommandFactoryException($"Couldn't create object of \" {objectType} \" instance, wrong function arguments");
                }
            }
            else
            {
                throw new CommandFactoryException("There no such command");
            }
        }
        private string[] getAllCtorsParamTypes(Type obj)
        {
            List<string> ctorsTypes = new List<string>();

            var ctors = obj.GetConstructors();
            foreach (var ctor in ctors)
            {
                foreach (var parameter in ctor.GetParameters())
                {
                    ctorsTypes.Add(parameter.ParameterType.Name);
                }
            }
            return ctorsTypes.ToArray();
        }
    }
}
