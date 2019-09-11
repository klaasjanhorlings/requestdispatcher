using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher
{
    public class BaseRequestDispatcher : IRequestDispatcher
    {
        private readonly HttpClient httpClient;

        private IContentSerializer contentSerializer;
        public IContentSerializer ContentSerializer
        {
            get => contentSerializer;
            set => contentSerializer = value ?? throw new ArgumentNullException(nameof(ContentSerializer));
        }

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
            request.Content = ContentSerializer.Serialize(body);

            await httpClient.SendAsync(request, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var request = new HttpRequestMessage(method, path);
            var response = await httpClient.SendAsync(request, cancellationToken);
            var result = await ContentSerializer.DeserializeAsync<TResponse>(response.Content);

            return await Task.FromResult(result);
        }

        public Task<TResponse> SendAsync<TResponse, TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
