using System;
using System.Net;
using System.Net.Http;

namespace UserCom.Http
{
    public class UserComClientException : Exception
    {
        public HttpMethod? Method { get; }
        public string? RequestUri { get; }
        public HttpStatusCode StatusCode { get; }
        public string? ReasonPhrase { get; }
        public string? ErrorMessage { get; }

        public UserComClientException() { }

        public UserComClientException(string message) : base(message) { }

        public UserComClientException(string message, Exception innerException) : base(message, innerException) { }

        public UserComClientException(HttpMethod method, string requestUri, HttpStatusCode statusCode, string reasonPhrase, string errorMessage, Exception? innerException = null)
            : base($"{method}: {requestUri} {statusCode:G} '{reasonPhrase}' '{errorMessage}'", innerException)
        {
            Method = method;
            RequestUri = requestUri;
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
            ErrorMessage = errorMessage;
        }
    }
}