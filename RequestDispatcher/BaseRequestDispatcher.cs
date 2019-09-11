using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher
{
    public class BaseRequestDispatcher : IRequestDispatcher
    {
        public Task SendAsync(HttpMethod method, string path, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync<TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> SendAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> SendAsync<TResponse, TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
