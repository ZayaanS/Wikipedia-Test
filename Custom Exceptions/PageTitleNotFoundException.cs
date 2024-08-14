
namespace Wikipedia_Testing.Custom_Exceptions
{
    /* Custom exception class to represent a scenario where the expected page title is not found.
       from the base CustomExceptionBase class for consistency. */
    internal class PageTitleNotFoundException : CustomExceptionBase
    { 
        public PageTitleNotFoundException() : base() { } // Default Parameterless constructor for the PageTitleNotFoundException class.
        public PageTitleNotFoundException(string message) : base(message) { } // Constructor for the class that takes a custom error message
        public PageTitleNotFoundException(string message, Exception innerException) : base(message, innerException) { } // Constructor for the class that takes a custom error message and an inner exception.
    }
}