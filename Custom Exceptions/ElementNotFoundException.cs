
namespace Wikipedia_Testing.Custom_Exceptions
{
    /* Custom exception class to represent scenarios where an expected element is not found on the page.
       Inherits from the base CustomExceptionBase class for consistency */
    internal class ElementNotFoundException : CustomExceptionBase
    {
        public ElementNotFoundException() : base() { } // Default Parameterless constructor for the ElementNotFoundException class
        public ElementNotFoundException(string message) : base(message) { } // Constructor for the class that takes a custom error message
        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException) { } // Constructor for the class that takes a custom error message and an inner exception
    }
}
