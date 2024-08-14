using System;

namespace Wikipedia_Testing.Custom_Exceptions
{
    internal class CustomExceptionBase : Exception
    {
        // Parameterless constructor
        public CustomExceptionBase() : base() { }

        // Constructor with a string message
        public CustomExceptionBase(string message) : base(message) { }

        // Constructor with a string message and an inner exception
        public CustomExceptionBase(string message, Exception innerException) : base(message, innerException) { }
    }
}
