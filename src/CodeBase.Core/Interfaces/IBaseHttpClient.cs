using CodeBase.Core.ValueObjects;
using System.Threading.Tasks;

namespace CodeBase.Core.Interfaces
{
    public interface IBaseHttpClient
    {
        Task<ResponseContext<TResponse>> PostAsync<TRequest, TResponse>(string url, RequestContext<TRequest> requestContext, RequestOption option = null)
            where TRequest : BaseRequestData
            where TResponse : BaseResponseData;

        Task<ResponseContext<TResponse>> GetAsync<TResponse>(string url, RequestOption option = null) where TResponse : BaseResponseData;
    }
}
