using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Test.RequestDispatcher
{
    [TestClass]
    public class SendAsync
    {
        [TestMethod]
        [DataRow("get")]
        [DataRow("post")]
        [DataRow("put")]
        [DataRow("head")]
        public async Task SetsHttpMethodToRequestMessage(string methodName)
        {
            // Arrange
            var messageHandler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(messageHandler);
            var requestDispatcher = new BaseRequestDispatcher(httpClient);
            var method = new HttpMethod(methodName);

            // Act
            await requestDispatcher.SendAsync(method, "http://localhost");

            // Assert
            Assert.AreEqual(1, messageHandler.CallCount);
            Assert.AreEqual(method, messageHandler.LastRequestMessage.Method);
        }
    }
}
