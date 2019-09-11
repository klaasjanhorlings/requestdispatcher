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
    public class SendAsyncWithResponseContent : SendContentBase
    {
        protected override async Task CallSend(HttpMethod method, string path, CancellationToken token = default)
            => sendResult = await requestDispatcher.SendAsync<object>(method, path, token);

        [TestMethod]
        public override async Task CallsDeserializeWithReturnedContent() => await base.CallsDeserializeWithReturnedContent();

        [TestMethod]
        public override async Task ReturnsDeserializedResult() => await base.ReturnsDeserializedResult();
    }
}
