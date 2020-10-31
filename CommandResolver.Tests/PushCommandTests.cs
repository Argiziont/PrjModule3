using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
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

        #region snippet_Constructor_Passes_InputIsStringPairStack
        [Fact]
        public void Constructor_Passes_InputIsStringStringPairStack()
        {
            // Arrange
            string variable = "test";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            var result = Record.Exception(() => new PushCommand(variable, ref pair, ref stack));

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
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            void result() => new PushCommand(variable, ref pair, ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputStackIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputStackIsNull()
        {
            // Arrange
            string variable = "test";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = null;

            // Act
            void result() => new PushCommand(variable, ref pair, ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputPairIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputPairIsNull()
        {
            // Arrange
            string variable = "test";
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            void result() => new PushCommand(variable, ref pair, ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_Passes_InputOperationIsCorrect
        [Fact]
        public void Run_Passes_InputOperationIsCorrect()
        {
            // Arrange
            string variable = "test";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>("test", 5);
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            var push = new PushCommand(variable, ref pair, ref stack);

            // Act
            push.Run();

            // Assert
            Assert.Equal(pair.Id, stack.Peek().Id);
            Assert.Equal(pair.Value, stack.Peek().Value);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputVariableNameIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputVariableNameIsIncorrect()
        {
            // Arrange
            string variable = "";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>("test", 5);
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            var push = new PushCommand(variable, ref pair, ref stack);

            // Act
            void result() => push.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputPairIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputPairIsIncorrect()
        {
            // Arrange
            string variable = "test";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            var push = new PushCommand(variable, ref pair, ref stack);

            // Act
            void result() => push.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
    }
}
