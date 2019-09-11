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
    public class SendAsyncWithRequestAndResponseContent : SendContentBase
    {
        protected override async Task CallSend(HttpMethod method, string path, CancellationToken token = default)
            => sendResult = await requestDispatcher.SendAsync<object, object>(method, path, requestContent, token);

        [TestMethod]
        public override async Task CallsSerializeWithPassedContent() => await base.CallsSerializeWithPassedContent();

        [TestMethod]
        public override async Task SetsSerializedContentToRequestMessage() => await base.SetsSerializedContentToRequestMessage();

        [TestMethod]
        public override async Task CallsDeserializeWithReturnedContent() => await base.CallsDeserializeWithReturnedContent();

        [TestMethod]
        public override async Task ReturnsDeserializedResult() => await base.ReturnsDeserializedResult();
    }
}
