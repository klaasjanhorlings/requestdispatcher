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


        [TestMethod]
        [DataRow("http://localhost")]
        [DataRow("https://www.capteur.nl")]
        [DataRow("http://goatse.cx")]
        public async Task SetsPathToRequestMessage(string path)
        {
            // Arrange
            var messageHandler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(messageHandler);
            var requestDispatcher = new BaseRequestDispatcher(httpClient);

            // Act
            await requestDispatcher.SendAsync(HttpMethod.Get, path);

            // Assert
            Assert.AreEqual(1, messageHandler.CallCount);
            Assert.AreEqual(new Uri(path), messageHandler.LastRequestMessage.RequestUri);
        }
    }
}
