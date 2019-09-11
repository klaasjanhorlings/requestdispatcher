using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher.Test.RequestDispatcher
{
    [TestClass]
    public class SendAsync
    {
        private TestHttpMessageHandler messageHandler;
        private HttpClient httpClient;
        private BaseRequestDispatcher requestDispatcher;

        [TestInitialize]
        public void Initialize()
        {
            messageHandler = new TestHttpMessageHandler();
            httpClient = new HttpClient(messageHandler);
            requestDispatcher = new BaseRequestDispatcher(httpClient);
        }

        [TestMethod]
        [DataRow("get")]
        [DataRow("post")]
        [DataRow("put")]
        [DataRow("head")]
        public async Task SetsHttpMethodToRequestMessage(string methodName)
        {
            // Arrange
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
            // Act
            await requestDispatcher.SendAsync(HttpMethod.Get, path);

            // Assert
            Assert.AreEqual(1, messageHandler.CallCount);
            Assert.AreEqual(new Uri(path), messageHandler.LastRequestMessage.RequestUri);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task ThrowsWhenPassedCancelledToken()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            await requestDispatcher.SendAsync(HttpMethod.Get, "http://localhost", cts.Token);
        }
    }
}
