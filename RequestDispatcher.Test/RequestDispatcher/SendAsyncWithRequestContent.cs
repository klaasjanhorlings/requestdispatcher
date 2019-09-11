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
            // Act
            await requestDispatcher.SendAsync(HttpMethod.Get, "http://localhost", requestContent);

            // Assert
            serializerMock.VerifyAll();
        }

        [TestMethod]
        public async Task SetsSerializedContentToRequestMessage()
        {
            // Act
            await requestDispatcher.SendAsync(HttpMethod.Get, "http://localhost", requestContent);

            // Assert
            Assert.AreEqual(serializedRequestContent, messageHandler.LastRequestMessage.Content);
        }
    }
}
