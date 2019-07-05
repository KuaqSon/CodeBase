using CodeBase.Core.ValueObjects;
using System.Net;
using System.Web.Http;

namespace CodeBase.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        protected ResponseContext<TResponse> OkResponse<TResponse>(TResponse data)
            where TResponse : IResponseData
        {
            return new ResponseContext<TResponse>(data)
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
