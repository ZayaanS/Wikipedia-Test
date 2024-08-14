
namespace Wikipedia_Testing.Custom_Exceptions
{
    /* Custom exception class to represent navigation failures within the application.
       Inherits from the base CustomExceptionBase class for consistency. */
    internal class NavigationException : CustomExceptionBase
    {
        public NavigationException() : base() { } // Default Parameterless constructor for the NavigationException class
        public NavigationException(string message) : base(message) { } // Constructor for the class that takes a custom error message
        public NavigationException(string message, Exception innerException) : base(message, innerException) { } // Constructor for the class that takes a custom error message and an inner exception
    }
}