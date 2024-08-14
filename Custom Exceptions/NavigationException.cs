
namespace Wikipedia_Testing.Custom_Exceptions
{
    internal class NavigationException : CustomExceptionBase
    {
        // Parameterless constructor
        public NavigationException() : base() { }

        // Constructor with a string message
        public NavigationException(string message) : base(message) { }

        // Constructor with a string message and an inner exception
        public NavigationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
