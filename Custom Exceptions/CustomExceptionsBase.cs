
namespace Wikipedia_Testing.Custom_Exceptions
{
    /* Base class for custom exceptions within the Wikipedia Testing project. 
       Provides common constructors for custom exception handling. */
    internal class CustomExceptionBase : Exception
    {
        public CustomExceptionBase() : base() { } // Default Parameterless constructor
        public CustomExceptionBase(string message) : base(message) { } // Constructor that takes a custom error message
        public CustomExceptionBase(string message, Exception innerException) : base(message, innerException) { } // Constructor that takes a custom error message and an inner exception
    }
}
