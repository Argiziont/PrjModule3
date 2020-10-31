using CommandResolver.Helpers;
using System;
using System.Collections.Generic;

namespace CommandResolver.Exceptions
{
    public class CommandExecutionException : Exception
    {
        public MutableKeyValuePair<string, object> ExceptionPair { get; private set; }
        public Stack<MutableKeyValuePair<string, object>> ExceptionStack { get; private set; }
        public CommandExecutionException(string message,
            MutableKeyValuePair<string, object> pair,
            Stack<MutableKeyValuePair<string, object>> stack) : base(message)
        {
            ExceptionPair = pair;
            ExceptionStack = stack;
        }
        public CommandExecutionException(string message,
            MutableKeyValuePair<string, object> pair) : this(message, pair, null) { }
        public CommandExecutionException(string message,
            Stack<MutableKeyValuePair<string, object>> stack) : this(message, null, stack) { }
        public CommandExecutionException(string message) : this(message, null, null) { }
        public CommandExecutionException() : this(null, null, null) { }
    }
}
