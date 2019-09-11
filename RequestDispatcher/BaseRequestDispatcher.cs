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

            var request = new HttpRequestMessage(method, path);
            await SendAsync(request, cancellationToken);
        }

        public async Task SendAsync<TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, path);            
            request.Content = ContentSerializer.Serialize(body);

            await SendAsync(request, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, path);

            var response = await SendAsync(request, cancellationToken);
            var result = await ContentSerializer.DeserializeAsync<TResponse>(response.Content);

            return await Task.FromResult(result);
        }

        public async Task<TResponse> SendAsync<TResponse, TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, path);
            request.Content = contentSerializer.Serialize(body);

            var response = await SendAsync(request, cancellationToken);
            var result = await ContentSerializer.DeserializeAsync<TResponse>(response.Content);

            return await Task.FromResult(result);
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpErrorResponseException(response.StatusCode);
            }

            return response;
        }
    }
}
