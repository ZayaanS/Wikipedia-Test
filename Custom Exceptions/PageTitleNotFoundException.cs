
namespace Wikipedia_Testing.Custom_Exceptions
{
    internal class PageTitleNotFoundException : CustomExceptionBase
    {
        // Parameterless constructor
        public PageTitleNotFoundException() : base() { }

        // Constructor with a string message
        public PageTitleNotFoundException(string message) : base(message) { }

        // Constructor with a string message and an inner exception
        public PageTitleNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
