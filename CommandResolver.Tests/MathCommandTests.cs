using CommandResolver.Commands;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using System.Collections.Generic;
using Xunit;

namespace CommandResolver.Tests
{
    public class MathCommandTests
    {
        public MathCommandTests()
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
            string operation = "+";
            CommandContext context = new CommandContext();

            // Act
            var result = Record.Exception(() => new MathCommand(operation, context));

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputOperationIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputOperationIsNull()
        {
            // Arrange
            string operation = null;
            CommandContext context = new CommandContext();

            // Act
            void result() => new MathCommand(operation, context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfCommandContextIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfCommandContextIsNull()
        {
            // Arrange
            string operation = "+";
            CommandContext context = null;

            // Act
            void result() => new MathCommand(operation,context);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

        #region snippet_Run_AddsVariable_InputOperationIsPlus
        [Fact]
        public void Run_AddsVariable_InputOperationIsPlus()
        {
            // Arrange
            string operation = "+";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 5));
            context.PushStack(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 10;
            var expectedName = "ans";
            var math = new MathCommand(operation, context);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, context.Stack.Peek().Value);
            Assert.Equal(expectedName, context.PeekLastStackElement().Id);
        }
        #endregion
        #region snippet_Run_MultiplyVariable_InputOperationIsMultiply
        [Fact]
        public void Run_MultiplyVariable_InputOperationIsMultiply()
        {
            // Arrange
            string operation = "*";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 5));
            context.PushStack(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 25;
            var expectedName = "ans";
            var math = new MathCommand(operation, context);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, context.Stack.Peek().Value);
            Assert.Equal(expectedName, context.PeekLastStackElement().Id);
        }
        #endregion
        #region snippet_Run_DividesVariable_InputOperationIsDivide
        [Fact]
        public void Run_DividesVariable_InputOperationIsDivide()
        {
            // Arrange
            string operation = "/";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 5));
            context.PushStack(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 1;
            var expectedName = "ans";
            var math = new MathCommand(operation, context);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, context.Stack.Peek().Value);
            Assert.Equal(expectedName, context.PeekLastStackElement().Id);
        }
        #endregion
        #region snippet_Run_DifferenceVariable_InputOperationIsDifference
        [Fact]
        public void Run_DifferenceVariable_InputOperationIsDifference()
        {
            // Arrange
            string operation = "-";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 5));
            context.PushStack(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 0;
            var expectedName = "ans";
            var math = new MathCommand(operation, context);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, context.Stack.Peek().Value);
            Assert.Equal(expectedName, context.PeekLastStackElement().Id);
        }
        #endregion
        #region snippet_Run_SqrtOfVariable_InputOperationIsSqrt
        [Fact]
        public void Run_SqrtOfVariable_InputOperationIsSqrt()
        {
            // Arrange
            string operation = "SQRT";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 25));

            double expectedNumber = 5;
            var expectedName = "ans";
            var math = new MathCommand(operation, context);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, context.Stack.Peek().Value);
            Assert.Equal(expectedName, context.PeekLastStackElement().Id);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputOperationIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputOperationIsIncorrect()
        {
            // Arrange
            string operation = "test";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 5));
            context.PushStack(new MutableKeyValuePair<string, object>("var2", 5));

            var math = new MathCommand(operation, context);

            // Act
            void result() => math.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputStackIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputStackIsIncorrect()
        {
            // Arrange
            string operation = "+";
            CommandContext context = new CommandContext();

            context.PushStack(new MutableKeyValuePair<string, object>("var1", 5));

            var math = new MathCommand(operation, context);

            // Act
            void result() => math.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

    }
}
