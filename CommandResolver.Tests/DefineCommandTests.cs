using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using System.Linq;
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

        #region snippet_Constructor_Passes_InputIsStringPairCommandContext
        [Fact]
        public void Constructor_Passes_InputIsStringPairCommandContext()
        {
            // Arrange
            string variable = "var";
            string number = "55";
            CommandContext context = new CommandContext();

            // Act
            var result = Record.Exception(() => new DefineCommand(variable, number, context));

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
            CommandContext context = new CommandContext();

            // Act
            void result() => new DefineCommand(variable, number, context);

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
            CommandContext context = new CommandContext();

            // Act
            void result() => new DefineCommand(variable, number, context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfCommandContextIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfCommandContextIsNull()
        {
            // Arrange
            string variable = "var";
            string number = "55";
            CommandContext context = null;

            // Act
            void result() => new DefineCommand(variable, number, context);

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
            CommandContext context = new CommandContext();
            var define = new DefineCommand(variable, number, context);

            // Act
            define.Run();
            var variableFromDefines = context.PairsList.SingleOrDefault(p => p.Id == "var");

            // Assert
            Assert.IsType<MutableKeyValuePair<string, object>>(variableFromDefines);
        }
        #endregion
        #region snippet_Run_Passes_InputStringIsLong
        [Fact]
        public void Run_Passes_InputStringIsLong()
        {
            // Arrange
            string variable = "var";
            string number = "55555555555";
            CommandContext context = new CommandContext();
            var define = new DefineCommand(variable, number, context);

            // Act
            define.Run();
            var variableFromDefines = context.PairsList.SingleOrDefault(p => p.Id == "var");

            // Assert
            Assert.IsType<MutableKeyValuePair<string, object>>(variableFromDefines);
        }
        #endregion
        #region snippet_Run_ThrowsExeption_InputStringIsIncorrect
        [Fact]
        public void Run_ThrowsExeption_InputStringIsIncorrect()
        {
            // Arrange
            string variable = "var";
            string number = "55test";
            CommandContext context = new CommandContext();
            var define = new DefineCommand(variable, number, context);

            // Act
            void result() => define.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

    }
}
