using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using Xunit;

namespace CommandResolver.Tests
{
    public class DefineCommandTests
    {
        public DefineCommandTests()
        {
            //Dot in console
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //
        }

        #region snippet_Constructor_Passes_InputIsStringStringPairStack
        [Fact]
        public void Constructor_Passes_InputIsStringStringPairStack()
        {
            // Arrange
            string variable = "var";
            string number = "55";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();

            // Act
            var result = Record.Exception(() => new DefineCommand(variable, number, ref pair));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputVariableNameIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputVariableNameIsNull()
        {
            // Arrange
            string variable = null;
            string number = "55";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();

            // Act
            void result() => new DefineCommand(variable, number, ref pair);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputNumberNameIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputNumberNameIsNull()
        {
            // Arrange
            string variable = "var";
            string number = null;
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();

            // Act
            void result() => new DefineCommand(variable, number, ref pair);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputKeyValuePairIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputKeyValuePairIsNull()
        {
            // Arrange
            string variable = "var";
            string number = "55";
            MutableKeyValuePair<string, object> pair = null;

            // Act
            void result() => new DefineCommand(variable, number, ref pair);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_Passes_InputStringIsDouble
        [Fact]
        public void Run_Passes_InputStringIsDouble()
        {
            // Arrange
            string variable = "var";
            string number = "55.5";
            double expectedNumber = 55.5;
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            var define = new DefineCommand(variable, number, ref pair);

            // Act
            define.Run();

            // Assert
            Assert.Equal(expectedNumber, pair.Value);
        }
        #endregion
        #region snippet_Run_Passes_InputStringIsLong
        [Fact]
        public void Run_Passes_InputStringIsLong()
        {
            // Arrange
            string variable = "var";
            string number = "55555555555";
            long expectedNumber = 55555555555;
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            var define = new DefineCommand(variable, number, ref pair);

            // Act
            define.Run();

            // Assert
            Assert.Equal(expectedNumber, pair.Value);
        }
        #endregion
        #region snippet_Run_ThrowsExeption_InputStringIsIncorrect
        [Fact]
        public void Run_ThrowsExeption_InputStringIsIncorrect()
        {
            // Arrange
            string variable = "var";
            string number = "55test";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            var define = new DefineCommand(variable, number, ref pair);

            // Act
            void result() => define.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

    }
}
