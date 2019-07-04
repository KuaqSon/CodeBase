using CodeBase.Core.Interfaces;
using CodeBase.Core.ValueObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace CodeBase.Infrastructure.Internet
{
    public abstract class BaseHttpClient : IBaseHttpClient
    {
        private static readonly RestClient Client = new RestClient();
        protected abstract string RoutePrefix { get; }

        protected BaseHttpClient(string uri)
        {
            Client.BaseUrl = new Uri(uri.TrimEnd('/') + "/");
            UpdateHeader("content-type", "application/json; charset=utf-8");
        }

        public async Task<ResponseContext<TResponse>> GetAsync<TResponse>(string url, RequestOption option = null)
            where TResponse : IResponseData
        {
            var req = new RestRequest(CallTo(url));
            if (option?.RequestParams?.Keys?.Count > 0)
            {
                foreach (var pair in option.RequestParams)
                {
                    req.AddQueryParameter(pair.Key, pair.Value.ToString(), true);
                }
            }

            var res = await Client.ExecuteTaskAsync<ResponseContext<TResponse>>(req);

            return ProcessResult(res);
        }

        public async Task<ResponseContext<TResponse>> PostAsync<TRequest, TResponse>(string url, RequestContext<TRequest> requestContext, RequestOption option = null)
            where TRequest : IRequestData
            where TResponse : IResponseData
        {
            var req = new RestRequest(CallTo(url));
            if (option == null)
            {
                req.Method = Method.POST;
            }
            else
            {
                switch (option.Method)
                {
                    case RequestMethod.Delete:
                        req.Method = Method.DELETE;
                        break;
                    case RequestMethod.Patch:
                        req.Method = Method.PATCH;
                        break;
                    case RequestMethod.Put:
                        req.Method = Method.PUT;
                        break;
                    default:
                        req.Method = Method.POST;
                        break;
                }
            }

            req.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(requestContext), ParameterType.RequestBody);

            if (!option?.SkipAuth ?? false)
                AddAuthenticationToken(requestContext.AuthToken);

            var res = await Client.ExecuteTaskAsync<ResponseContext<TResponse>>(req);

            return ProcessResult(res);
        }

        private ResponseContext<TResponse> ProcessResult<TResponse>(IRestResponse<ResponseContext<TResponse>> response)
            where TResponse : IResponseData
        {
            if (response.ErrorException != null)
            {
                throw new Exception($"Could not connect to the remote api at {Client.BaseHost}");
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResponseContext<TResponse>
                {
                    IsError = true,
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<TResponse>(response.Content);
                return new ResponseContext<TResponse>(content)
                {
                    StatusCode = HttpStatusCode.OK
                };
            }

            // api throws exception
            var error = JsonConvert.DeserializeObject<ExceptionResponse>(response.Content);
            return new ResponseContext<TResponse>
            {
                IsError = true,
                StatusCode = HttpStatusCode.InternalServerError,
                Error = error
            };
        }

        private void UpdateHeader(string name, string value)
        {
            var parameter = Client.DefaultParameters?.FirstOrDefault(x => x.Name == name && x.Type == ParameterType.HttpHeader);

            if (parameter == null)
                Client.AddDefaultHeader(name, value);
            else
                parameter.Value = value;
        }

        private void AddAuthenticationToken(string token, string tokenType = "Bearer")
        {
            var auth = Client.DefaultParameters?.FirstOrDefault(x => x.Name == "Authorization");

            if (string.IsNullOrWhiteSpace(token))
            {
                // in some cases the api does not require authentication
                // so we should remove the authrozation token to prevent api to check
                if (auth != null)
                    Client.RemoveDefaultParameter("Authorization");
                return;
            }

            var bearer = $"{tokenType} {token}";

            if (auth == null)
            {
                Client.AddDefaultHeader("Authorization", bearer);
            }
            else
            {
                auth.Value = bearer;
            }
        }

        private string CallTo(string url) => RoutePrefix.TrimEnd('/') + "/" + url.TrimStart('/');

        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                let value = p.GetValue(obj, null)
                where value != null
                select p.Name + "=" + HttpUtility.UrlEncode(value.ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}
