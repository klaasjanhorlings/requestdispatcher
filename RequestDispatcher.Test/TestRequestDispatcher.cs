using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Test
{
    class TestRequestDispatcher : BaseRequestDispatcher
    {
        public int SetHeadersCallCount { get; private set; } = 0;
        public HttpRequestMessage LastSetHeadersMessage { get; private set; }

        public int OnErrorResponseCallCount { get; private set; } = 0;
        public HttpResponseMessage LastOnErrorResponseMessage { get; private set; }

        public TestRequestDispatcher(HttpClient httpClient) : base(httpClient)
        {

        }

        protected override Task OnErrorResponse(HttpResponseMessage response)
        {
            OnErrorResponseCallCount++;
            LastOnErrorResponseMessage = response;

            return base.OnErrorResponse(response);
        }

        protected override void SetHeaders(HttpRequestMessage request)
        {
            SetHeadersCallCount++;
            LastSetHeadersMessage = request;

            base.SetHeaders(request);
        }
    }
}
