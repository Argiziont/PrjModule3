using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace CommandResolver.Tests
{
    public class CommandFactoryTests
    {
        public CommandFactoryTests()
        {
            //Dot in console
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //
        }

        #region snippet_Constructor_Passes_InputIsCommandContext
        [Fact]
        public void Constructor_Passes_InputIsCommandContext()
        {
            // Arrange
            CommandContext context = new CommandContext();

            // Act
            var result = Record.Exception(() => new CommandFactory(context));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfCommandContextIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputStackIsNull()
        {
            // Arrange
            CommandContext context = null;

            // Act
            void result() => new CommandFactory(context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_GetCommand_ReturnsObjectOfICommand_InputCommandIsCorrect
        [Fact]
        public void GetCommand_ReturnsICommand_InputCommandIsCorrect()
        {
            // Arrange
            string command = "DEFINE a 5";
            CommandContext context = new CommandContext();
            var factory = new CommandFactory(context);

            // Act
            var result = factory.GetCommand(command);

            // Assert
            Assert.IsType<DefineCommand>(result);
        }
        #endregion
        #region snippet_GetCommand_ThrowsException_InputCommandHasWrongNumberOfparams
        [Fact]
        public void GetCommand_ThrowsException_InputCommandHasWrongNumberOfparams()
        {
            // Arrange
            string command = "DEFINE a 5 test";
            CommandContext context = new CommandContext();
            var factory = new CommandFactory(context);

            // Act
            void result ()=> factory.GetCommand(command);

            // Assert
            Assert.Throws<CommandFactoryException>(result);
        }
        #endregion
        #region snippet_GetCommand_ThrowsException_InputCommandCallsWrongFunction
        [Fact]
        public void GetCommand_ThrowsException_InputCommandCallsWrongFunction()
        {
            // Arrange
            string command = "TEST 6";
            CommandContext context = new CommandContext();
            var factory = new CommandFactory(context);

            // Act
            void result() => factory.GetCommand(command);

            // Assert
            Assert.Throws<CommandFactoryException>(result);
        }
        #endregion

    }
}
