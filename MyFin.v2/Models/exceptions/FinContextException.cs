namespace MyFin.v2.Models.exceptions
{
    public class FinContextException : Exception
    {
        public FinContextException(string message, Exception innerException) : base(message, innerException) { }
        public FinContextException(string message) : base(message) { }
    }
}
