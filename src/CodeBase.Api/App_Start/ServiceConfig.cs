using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CodeBase.Api
{
    public class ServiceConfig
    {
        public static void Configure()
        {
            GlobalConfiguration.Configuration
                .Services
                .Replace(typeof(IExceptionHandler), new ApiExceptionHandler());
        }
    }
}