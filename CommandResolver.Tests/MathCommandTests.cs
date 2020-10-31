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
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            var result = Record.Exception(() => new MathCommand(operation, pair, ref stack));

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
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            // Act
            void result() => new MathCommand(operation, pair, ref stack);

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion
        #region snippet_Constructor_ThrowsExeption_IfInputStackIsNull
        [Fact]
        public void Constructor_ThrowsExeption_IfInputStackIsNull()
        {
            // Arrange
            string operation = "+";
            MutableKeyValuePair<string, object> pair = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> stack = null;

            // Act
            void result() => new MathCommand(operation, pair, ref stack);

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
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 5));
            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 10;
            var expectedName = "ans";
            var math = new MathCommand(operation, pair, ref stack);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, stack.Peek().Value);
            Assert.Equal(expectedName, stack.Peek().Id);
        }
        #endregion
        #region snippet_Run_MultiplyVariable_InputOperationIsMultiply
        [Fact]
        public void Run_MultiplyVariable_InputOperationIsMultiply()
        {
            // Arrange
            string operation = "*";
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 5));
            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 25;
            var expectedName = "ans";
            var math = new MathCommand(operation, pair, ref stack);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, stack.Peek().Value);
            Assert.Equal(expectedName, stack.Peek().Id);
        }
        #endregion
        #region snippet_Run_DividesVariable_InputOperationIsDivide
        [Fact]
        public void Run_DividesVariable_InputOperationIsDivide()
        {
            // Arrange
            string operation = "/";
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 5));
            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 1;
            var expectedName = "ans";
            var math = new MathCommand(operation, pair, ref stack);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, stack.Peek().Value);
            Assert.Equal(expectedName, stack.Peek().Id);
        }
        #endregion
        #region snippet_Run_DifferenceVariable_InputOperationIsDifference
        [Fact]
        public void Run_DifferenceVariable_InputOperationIsDifference()
        {
            // Arrange
            string operation = "-";
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 5));
            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));

            double expectedNumber = 0;
            var expectedName = "ans";
            var math = new MathCommand(operation, pair, ref stack);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, stack.Peek().Value);
            Assert.Equal(expectedName, stack.Peek().Id);
        }
        #endregion
        #region snippet_Run_SqrtOfVariable_InputOperationIsSqrt
        [Fact]
        public void Run_SqrtOfVariable_InputOperationIsSqrt()
        {
            // Arrange
            string operation = "SQRT";
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 25));

            double expectedNumber = 5;
            var expectedName = "ans";
            var math = new MathCommand(operation, pair, ref stack);

            // Act
            math.Run();

            // Assert
            Assert.Equal(expectedNumber, stack.Peek().Value);
            Assert.Equal(expectedName, stack.Peek().Id);
        }
        #endregion
        #region snippet_Run_ThrowsException_InputOperationIsIncorrect
        [Fact]
        public void Run_ThrowsException_InputOperationIsIncorrect()
        {
            // Arrange
            string operation = "test";
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 5));
            stack.Push(new MutableKeyValuePair<string, object>("var2", 5));

            var math = new MathCommand(operation, pair, ref stack);

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
            MutableKeyValuePair<string, object> pair = null;
            Stack<MutableKeyValuePair<string, object>> stack = new Stack<MutableKeyValuePair<string, object>>();

            stack.Push(new MutableKeyValuePair<string, object>("var1", 5));

            var math = new MathCommand(operation, pair, ref stack);

            // Act
            void result() => math.Run();

            // Assert
            Assert.Throws<CommandExecutionException>(result);
        }
        #endregion

    }
}
