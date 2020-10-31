using System;

namespace CommandResolver.Exceptions
{
    public class CommandFactoryException : Exception
    {
        public CommandFactoryException() : base() { }
        public CommandFactoryException(string message) : base(message) { }
    }
}
