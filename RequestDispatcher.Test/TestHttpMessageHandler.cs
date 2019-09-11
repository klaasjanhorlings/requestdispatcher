using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher.Test
{
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        public int CallCount { get; private set; } = 0;
        public HttpRequestMessage LastRequestMessage { get; private set; }
        public HttpResponseMessage LastResponseMessage { get; private set; }
        public Func<HttpRequestMessage, HttpResponseMessage> ResponseHandler { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CallCount++;

            LastRequestMessage = request;
            LastResponseMessage = ResponseHandler?.Invoke(request) ?? new HttpResponseMessage(HttpStatusCode.OK);

            return Task.FromResult(LastResponseMessage);
        }
    }
}
