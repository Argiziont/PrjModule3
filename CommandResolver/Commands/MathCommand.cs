using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class MathCommand : ICommand
    {
        public Stack<MutableKeyValuePair<string, object>> MainStack { get; private set; }
        public string MathOperation { get; private set; }

        /// <summary>
        /// Processes last one or two variables from stack
        /// </summary>
        /// <param name="operaion">Your operation name</param>
        /// <param name="stack">Main stack of programm where all varibles are stored</param>
        public MathCommand(string operaion, ref Stack<MutableKeyValuePair<string, object>> stack)
        {
            MathOperation = operaion ?? throw new CommandExecutionException("Math operation must be defined");
            MainStack = stack ?? throw new CommandExecutionException("Could process this if Stack isn't defined");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            double[] procNumbers;
            switch (MathOperation)
            {
                case "*":
                    try { procNumbers = GetLastTwoElements(); } catch (CommandExecutionException ex) { throw ex; }
                    MainStack.Push(new MutableKeyValuePair<string, object>("ans", (procNumbers[0] * procNumbers[1])));
                    break;
                case "/":
                    try { procNumbers = GetLastTwoElements(); } catch (CommandExecutionException ex) { throw ex; }
                    MainStack.Push(new MutableKeyValuePair<string, object>("ans", (procNumbers[0] / procNumbers[1])));
                    break;
                case "+":
                    try { procNumbers = GetLastTwoElements(); } catch (CommandExecutionException ex) { throw ex; }
                    MainStack.Push(new MutableKeyValuePair<string, object>("ans", (procNumbers[0] + procNumbers[1])));
                    break;
                case "-":
                    try { procNumbers = GetLastTwoElements(); } catch (CommandExecutionException ex) { throw ex; }
                    MainStack.Push(new MutableKeyValuePair<string, object>("ans", (procNumbers[0] - procNumbers[1])));
                    break;
                case "SQRT":
                    MutableKeyValuePair<string, object> number;
                    if (MainStack.TryPeek(out number))
                    {
                        MainStack.Push(new MutableKeyValuePair<string, object>("ans", Math.Sqrt(Convert.ToDouble(number.Value))));
                    }
                    else
                        throw new CommandExecutionException("Not enough items in stack to do this", MainStack);
                    break;
                default:
                    throw new CommandExecutionException($"There no such operation: {MathOperation}", MainStack);
            }
        }
        private double[] GetLastTwoElements()
        {
            List<double> numbersArray = new List<double>();
            if (MainStack.TryPop(out MutableKeyValuePair<string, object> firstNumber) &&
                MainStack.TryPeek(out MutableKeyValuePair<string, object> secondNumber))
            {
                numbersArray.Add(Convert.ToDouble(secondNumber.Value));
                numbersArray.Add(Convert.ToDouble(firstNumber.Value));
                MainStack.Push(new MutableKeyValuePair<string, object>(firstNumber.Id, firstNumber.Value));
            }
            else
            {
                throw new CommandExecutionException("There no 2 elements in stack to calculate this", MainStack);
            }
            return numbersArray.ToArray();
        }
    }
}
