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
    public class SendAsyncWithRequestContent : SendContentBase
    {

        protected override async Task CallSend(HttpMethod method, string path, CancellationToken token = default)
            => await requestDispatcher.SendAsync(method, path, requestContent, token);

        [TestMethod]
        public override async Task CallsSerializeWithPassedContent() => await base.CallsSerializeWithPassedContent();

        [TestMethod]
        public override async Task SetsSerializedContentToRequestMessage() => await base.SetsSerializedContentToRequestMessage();
    }
}
