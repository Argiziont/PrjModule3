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

        #region snippet_Constructor_Passes_InputIsPairStack
        [Fact]
        public void Constructor_Passes_InputIsPairStack()
        {
            // Arrange
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            var result = Record.Exception(() => new PrintCommand(ref stack));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputStackIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputNumberNameIsNull()
        {
            // Arrange
            Stack<MutableKeyValuePair<string, object>> stack = null;

            // Act
            void result() => new PrintCommand(ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_Passes_InputOperationIsCorrect
        [Fact]
        public void Run_Passes_InputOperationIsCorrect()
        {
            // Arrange
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));

            var print = new PrintCommand(ref stack);

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
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            var print = new PrintCommand(ref stack);

            // Act
            void result() => print.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

    }
}
