
namespace Wikipedia_Testing.Custom_Exceptions
{
    internal class ElementNotFoundException : CustomExceptionBase
    {
        // Parameterless constructor
        public ElementNotFoundException() : base() { }

        // Constructor with a string message
        public ElementNotFoundException(string message) : base(message) { }

        // Constructor with a string message and an inner exception
        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
