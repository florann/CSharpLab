using System.Net;

namespace CodeEditor.Api.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpResponseException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpResponseException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
