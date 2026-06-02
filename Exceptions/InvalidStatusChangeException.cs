namespace CertificatesApp.Exceptions
{
    public class InvalidStatusChangeException : Exception
    {
        public InvalidStatusChangeException(string message) : base(message) { }
    }
}
