using CodeBase.Core.Interfaces;
using CodeBase.Core.ValueObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Internet
{
    public abstract class BaseHttpClient : IBaseHttpClient
    {
        private static readonly RestClient _httpClient = new RestClient();
        protected abstract string RoutePrefix { get; }

        public Task<TResponse> GetAsync<TResponse>(string url, RequestOption option = null)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, RequestOption option = null)
            where TRequest : BaseRequestData
            where TResponse : BaseResponseData
        {
            throw new NotImplementedException();
        }
    }
}
