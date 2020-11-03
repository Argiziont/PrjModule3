using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace CommandResolver.Tests
{
    public class PushCommandTests
    {
        public PushCommandTests()
        {
            //Dot in console
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //
        }

        #region snippet_Constructor_Passes_InputIsStringCommandContext
        [Fact]
        public void Constructor_Passes_InputIsStringCommandContext()
        {
            // Arrange
            string variable = "test";
            CommandContext context = new CommandContext();

            // Act
            var result = Record.Exception(() => new PushCommand(variable, context));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputOperationIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputOperationIsNull()
        {
            // Arrange
            string variable = null;
            CommandContext context = new CommandContext();

            // Act
            void result() => new PushCommand(variable, context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfCommandContextIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfCommandContextIsNull()
        {
            // Arrange
            string variable = "test";
            CommandContext context = null;

            // Act
            void result() => new PushCommand(variable, context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_Passes_InputOperationIsCorrect
        [Fact]
        public void Run_Passes_InputOperationIsCorrect()
        {
            // Arrange
            string variable = "testName";
            string value = "5";
            CommandContext context = new CommandContext();

            var push = new PushCommand(variable, value, context);

            // Act
            push.Run();

            // Assert
            Assert.Equal(variable, context.PeekLastStackElement().Id);
            Assert.Equal(Convert.ToInt64(value), context.PeekLastStackElement().Value);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputValueIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputValueIsIncorrect()
        {
            // Arrange
            string variable = "test";
            string value = "testnumber";
            CommandContext context = new CommandContext();

            var push = new PushCommand(variable, value, context);

            // Act
            void result() => push.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
    }
}
