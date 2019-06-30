using CodeBase.Core.ValueObjects;
using System.Threading.Tasks;

namespace CodeBase.Core.Interfaces
{
    public interface IBaseHttpClient
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, RequestOption option = null)
            where TRequest : BaseRequestData
            where TResponse : BaseResponseData;

        Task<TResponse> GetAsync<TResponse>(string url, RequestOption option = null);
    }
}
