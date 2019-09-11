using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RequestDispatcher.Test.RequestDispatcher
{
    [TestClass]
    public class SendAsync
    {
        [TestMethod]
        public void SetsHttpMethodToRequestMessage()
        {
            var httpClient = new HttpClient();
            var requestDispatcher = new BaseRequestDispatcher(httpClient);
            requestDispatcher.SendAsync(HttpMethod.Get, "http://localhost");

            // Check HttpClient.Send call arguments??
        }
    }
}
