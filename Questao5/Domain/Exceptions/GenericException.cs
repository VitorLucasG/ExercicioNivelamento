using System.Net;

namespace Questao5.Domain.Exceptions
{
    [Serializable]
    public class GenericException : Exception
    {
        public string Error { get; }

        public string Message { get; }

        public HttpStatusCode StatusCode { get; }

        public GenericException(string error, string message, HttpStatusCode statusCode) : base(message)
        {
            Error = error;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
