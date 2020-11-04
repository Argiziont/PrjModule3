using CommandResolver.Exceptions;
using System.Collections.Generic;

namespace CommandResolver.Helpers
{
    public class CommandContext
    {
        public MutableKeyValuePair<string, object> Pair { get; private set; }
        public List<MutableKeyValuePair<string, object>> PairsList { get; private set; }
        public Stack<MutableKeyValuePair<string, object>> Stack { get; private set; }

        /// <summary>
        /// Creates context for factory
        /// </summary>
        public CommandContext()
        {
            Pair = new MutableKeyValuePair<string, object>();
            Stack = new Stack<MutableKeyValuePair<string, object>>();
            PairsList = new List<MutableKeyValuePair<string, object>>();
        }

        /// <summary>
        /// Intesrts variable with name and variable in Pair
        /// </summary>
        /// <param name="variableName">Your variable name</param>
        /// <param name="variableValue">Your variable value</param>
        public void AddVariable(string variableName, object variableValue)
        {
            PairsList.Add(new MutableKeyValuePair<string, object>(variableName, variableValue));
        }

        /// <summary>
        /// Adds element to stack
        /// </summary>
        /// <param name="variableName">Your variable name</param>
        /// <param name="variableValue">Your variable value</param>
        public void PushStack(string variableName, object variableValue)
        {
            Stack.Push(new MutableKeyValuePair<string, object>(variableName, variableValue));
        }

        /// <summary>
        /// Adds element to stack
        /// </summary>
        /// <param name="pair">Your pair with name and value</param>
        public void PushStack(MutableKeyValuePair<string, object> pair)
        {
            Stack.Push(new MutableKeyValuePair<string, object>(pair.Id, pair.Value));
        }

        /// <summary>
        /// Adds element to stack
        /// </summary>
        public void PushStack()
        {
            Stack.Push(new MutableKeyValuePair<string, object>(Pair.Id, Pair.Value));
        }

        /// <summary>
        /// Adds element to stack and removes last 2
        /// </summary>
        /// <param name="pair">Your pair with name and value</param>
        public void PushStackWithDel(MutableKeyValuePair<string, object> pair)
        {
            Stack.Pop();
            Stack.Pop();
            Stack.Push(new MutableKeyValuePair<string, object>(pair.Id, pair.Value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns last element from stack and removes it</returns>
        public MutableKeyValuePair<string, object> PopLastStackElement()
        {
            if (Stack.TryPop(out MutableKeyValuePair<string, object> element))
                return element;
            else
                throw new CommandExecutionException("Couldn't get last element");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns last element from stack without removing</returns>
        public MutableKeyValuePair<string, object> PeekLastStackElement()
        {
            if (Stack.TryPeek(out MutableKeyValuePair<string, object> element))
                return element;
            else
                throw new CommandExecutionException("Couldn't get last element");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns 2 last elements from stack without removing</returns>
        public MutableKeyValuePair<string, object>[] PeekLastTwoStackElement()
        {
            List<MutableKeyValuePair<string, object>> numbersArray = new List<MutableKeyValuePair<string, object>>();
            if (Stack.TryPop(out MutableKeyValuePair<string, object> firstNumber) &&
                Stack.TryPeek(out MutableKeyValuePair<string, object> secondNumber))
            {
                numbersArray.Add(secondNumber);
                numbersArray.Add(firstNumber);
                Stack.Push(new MutableKeyValuePair<string, object>(firstNumber.Id, firstNumber.Value));
            }
            else
            {
                throw new CommandExecutionException("There no 2 elements in stack to calculate this", Stack);
            }
            return numbersArray.ToArray();
        }
    }
}
