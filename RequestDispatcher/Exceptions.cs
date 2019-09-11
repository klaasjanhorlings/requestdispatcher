using System;
using System.Net;

namespace RequestDispatcher
{
    [Serializable]
    public class HttpErrorResponseException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public HttpErrorResponseException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpErrorResponseException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpErrorResponseException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            HttpStatusCode = httpStatusCode;
        }

        protected HttpErrorResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
