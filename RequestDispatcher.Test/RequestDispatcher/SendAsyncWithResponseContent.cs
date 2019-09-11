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
    public class SendAsyncWithResponseContent : SendBase
    {
        protected override async Task CallSend(HttpMethod method, string path, CancellationToken token = default)
            => sendResult = await requestDispatcher.SendAsync<object>(method, path, token);

        [TestMethod]
        public virtual async Task CallsDeserializeWithReturnedContent()
        {
            messageHandler.ResponseHandler = r => responseMessage;

            await CallSend(HttpMethod.Get, "http://localhost");

            Assert.AreEqual(1, messageHandler.CallCount);
            serializerMock.Verify(s => s.DeserializeAsync<object>(serializedResponseContent));
        }
    }
}
