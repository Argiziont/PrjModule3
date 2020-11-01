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

        #region snippet_Constructor_Passes_InputIsStringPairStack
        [Fact]
        public void Constructor_Passes_InputIsStringStringPairStack()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            var result = Record.Exception(() => new CommandFactory(ref pair, ref stack));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputStackIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputStackIsNull()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = null;

            // Act
            void result() => new CommandFactory(ref pair, ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputPairIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputPairIsNull()
        {
            // Arrange
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            void result() => new CommandFactory(ref pair, ref stack);

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
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();
            var factory = new CommandFactory(ref pair, ref stack);

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
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();
            var factory = new CommandFactory(ref pair, ref stack);

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
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();
            var factory = new CommandFactory(ref pair, ref stack);

            // Act
            void result() => factory.GetCommand(command);

            // Assert
            Assert.Throws<CommandFactoryException>(result);
        }
        #endregion

    }
}
