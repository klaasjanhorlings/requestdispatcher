using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
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

        protected object requestContent;
        protected StringContent serializedRequestContent;
        protected Mock<IContentSerializer> serializerMock;

        protected object responseContent;
        protected HttpResponseMessage responseMessage;
        protected HttpContent serializedResponseContent;

        protected object sendResult;

        protected abstract Task CallSend(HttpMethod method, string path, CancellationToken token = default(CancellationToken));

        [TestInitialize]
        public void InitializeBase()
        {
            messageHandler = new TestHttpMessageHandler();
            httpClient = new HttpClient(messageHandler);
            requestDispatcher = new BaseRequestDispatcher(httpClient);

            requestContent = new object();
            serializedRequestContent = new StringContent("request");

            responseContent = new object();
            serializedResponseContent = new StringContent("response");
            responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = serializedResponseContent;

            serializerMock = new Mock<IContentSerializer>();
            serializerMock.Setup(s => s.DeserializeAsync<object>(serializedResponseContent)).Returns(Task.FromResult(responseContent));
            serializerMock.Setup(s => s.Serialize(requestContent)).Returns(serializedRequestContent);
            requestDispatcher.ContentSerializer = serializerMock.Object;
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

        [TestMethod]
        [ExpectedException(typeof(HttpErrorResponseException))]
        public async Task ThrowsOnInvalidHttpStatus()
        {
            messageHandler.ResponseHandler = r => new HttpResponseMessage(HttpStatusCode.BadRequest);

            await CallSend(HttpMethod.Get, "http://localhost");
        }

        [TestMethod]
        public async Task SetsContentAndHttpStatusCodeToException()
        {
            var exceptionThrown = false;
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent("content");
            messageHandler.ResponseHandler = r => response;

            try
            {
                await CallSend(HttpMethod.Get, "http://localhost");
            }
            catch (HttpErrorResponseException e)
            {
                exceptionThrown = true;

                Assert.AreEqual("content", e.Message);
                Assert.AreEqual(HttpStatusCode.BadRequest, e.HttpStatusCode);
            }
            finally
            {
                Assert.IsTrue(exceptionThrown);
            }
        }
    }
}
