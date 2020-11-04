
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CommandResolver.Tests
{
    public class ProgramTests
    {
        #region snippet_Programm_Passes_InputIsReadFromFileAndCorrect
        [Fact]
        public void Programm_Passes_InputIsReadFromFileAndCorrect()
        {
            // Arrange
            CommandFactory commandFactory = new CommandFactory(new CommandContext());
            List<string> CommandList = new List<string>();

            if (File.Exists("CommandsForTest.tdat"))
            {
                string readText = File.ReadAllText("CommandsForTest.tdat");

                string[] linesFromFile = readText.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string commandLine in linesFromFile)
                {
                    if (commandLine != "")
                    {
                        CommandList.Add(commandLine);
                    }
                }
            }

            // Act&&Assert
            foreach (string command in CommandList)
            {
                ICommand executable = commandFactory.GetCommand(command);
                Assert.Null(Record.Exception(() => executable.Run()));
            }
        }
        #endregion
    }
}
