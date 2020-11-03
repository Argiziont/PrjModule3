using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using System.Collections.Generic;
using Xunit;

namespace CommandResolver.Tests
{
    public class PopCommandTests
    {
        public PopCommandTests()
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
            var result = Record.Exception(() => new PopCommand(context));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputCommandContextIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputCommandContextIsNull()
        {
            // Arrange
            CommandContext context = null;

            // Act
            void result() => new PopCommand(context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_Passes_InputOperationIsCorrect
        [Fact]
        public void Run_Passes_InputOperationIsCorrect()
        {
            // Arrange
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var2", 5));
            var stackSizeExpected = 0;

            var pop = new PopCommand(context);

            // Act
            pop.Run();

            // Assert
            Assert.Equal(stackSizeExpected, context.Stack.Count);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputStackIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputStackIsIncorrect()
        {
            // Arrange
            CommandContext context = new CommandContext();

            var pop = new PrintCommand(context);

            // Act
            void result() => pop.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
    }
}
