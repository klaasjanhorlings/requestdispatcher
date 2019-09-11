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
            // Arrange
            var messageHandler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(messageHandler);
            var requestDispatcher = new BaseRequestDispatcher(httpClient);

            // Act
            requestDispatcher.SendAsync(HttpMethod.Get, "http://localhost");

            // Assert
            Assert.AreEqual(1, messageHandler.CallCount);
            Assert.AreEqual(HttpMethod.Get, messageHandler.LastRequestMessage.Method);
        }
    }
}
