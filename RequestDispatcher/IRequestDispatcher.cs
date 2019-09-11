using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RequestDispatcher
{
    public interface IRequestDispatcher
    {
        Task SendAsync(HttpMethod method, string path, CancellationToken cancellationToken = default(CancellationToken));
        Task SendAsync<TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default(CancellationToken));

        Task<TResponse> SendAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken = default(CancellationToken));
        Task<TResponse> SendAsync<TResponse, TRequest>(HttpMethod method, string path, TRequest body, CancellationToken cancellationToken = default(CancellationToken));
    }
}
