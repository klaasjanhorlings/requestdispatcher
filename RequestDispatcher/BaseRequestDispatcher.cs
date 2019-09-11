using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher
{
    public class BaseRequestDispatcher : IRequestDispatcher
    {
        private readonly HttpClient httpClient;

        public BaseRequestDispatcher(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task SendAsync(HttpMethod method, string path, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var request = new HttpRequestMessage(method, path);
            await httpClient.SendAsync(request, cancellationToken);
        }

        public async Task SendAsync<TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var request = new HttpRequestMessage(method, path);
            await httpClient.SendAsync(request, cancellationToken);
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
