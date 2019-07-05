using CodeBase.Core.ValueObjects;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace CodeBase.Api.App_Start
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var message = context.Exception.Message;
            var meta = new ExceptionResponse
            {
                Message = message,
                RequestUri = context.Request.RequestUri,
                StackTrace = context.Exception.StackTrace
            };

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, meta);
            context.Result = new ResponseMessageResult(response);
        }
    }
}