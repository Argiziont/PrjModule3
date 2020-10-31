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

        #region snippet_Constructor_Passes_InputIsPairStack
        [Fact]
        public void Constructor_Passes_InputIsPairStack()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            var result = Record.Exception(() => new PopCommand(pair, ref stack));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputStackIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputNumberNameIsNull()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = null;

            // Act
            void result() => new PopCommand(pair, ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_Passes_InputOperationIsCorrect
        [Fact]
        public void Run_Passes_InputOperationIsCorrect()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));
            var stackSizeExpected = 0;

            var pop = new PopCommand(pair, ref stack);

            // Act
            pop.Run();

            // Assert
            Assert.Equal(stackSizeExpected, stack.Count);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputStackIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputStackIsIncorrect()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            var pop = new PrintCommand(pair, ref stack);

            // Act
            void result() => pop.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
    }
}
