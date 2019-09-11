using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher.Test.RequestDispatcher
{
    public abstract class SendBase
    {
        protected TestHttpMessageHandler messageHandler;
        protected HttpClient httpClient;
        protected BaseRequestDispatcher requestDispatcher;

        protected abstract Task CallSend(HttpMethod method, string path, CancellationToken token = default(CancellationToken));

        [TestInitialize]
        public void InitializeBase()
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
            await CallSend(method, "http://localhost");

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
            await CallSend(HttpMethod.Get, path);

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
            await CallSend(HttpMethod.Get, "http://localhost", cts.Token);
        }
    }
}
