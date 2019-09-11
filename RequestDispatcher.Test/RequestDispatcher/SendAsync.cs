using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher.Test.RequestDispatcher
{
    [TestClass]
    public class SendAsync : SendBase
    {
        protected override async Task CallSend(HttpMethod method, string path, CancellationToken token = default)
            => await requestDispatcher.SendAsync(method, path, token);
    }
}
