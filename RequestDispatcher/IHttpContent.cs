using System.Net.Http;
using System.Threading.Tasks;

namespace RequestDispatcher
{
    public interface IContentSerializer
    {
        HttpContent Serialize<T>(T content);
        Task<T> DeserializeAsync<T>(HttpContent content);
    }
}
