using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher.Test.RequestDispatcher
{
    [TestClass]
    public class SendAsyncWithRequestContent : SendBase
    {
        protected override async Task CallSend(HttpMethod method, string path, CancellationToken token = default)
            => await requestDispatcher.SendAsync(method, path, new object(), token);

        [TestMethod]
        public async Task CallsSerializeWithPassedContent()
        {
            // Arrange
            var requestContent = new object();
            var serializedRequestContent = new StringContent("request");
            var serializerMock = new Mock<IContentSerializer>();
            serializerMock.Setup(s => s.Serialize(requestContent)).Returns(serializedRequestContent);

            requestDispatcher.ContentSerializer = serializerMock.Object;

            // Act
            await requestDispatcher.SendAsync(HttpMethod.Get, "http://localhost", requestContent);

            // Assert
            serializerMock.VerifyAll();
        }
    }
}
