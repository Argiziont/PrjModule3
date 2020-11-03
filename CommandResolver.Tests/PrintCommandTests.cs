using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using System.Collections.Generic;
using Xunit;

namespace CommandResolver.Tests
{
    public class PrintCommandTests
    {
        public PrintCommandTests()
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
            var result = Record.Exception(() => new PrintCommand(context));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfCommandContextIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfCommandContextIsNull()
        {
            // Arrange
            CommandContext context = null;

            // Act
            void result() => new PrintCommand(context);

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

            var print = new PrintCommand(context);

            // Act
            var result = Record.Exception(() => print.Run());

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputStackIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputStackIsIncorrect()
        {
            // Arrange
            CommandContext context = new CommandContext();

            var print = new PrintCommand(context);

            // Act
            void result() => print.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

    }
}
