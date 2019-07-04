using System.Net;

namespace CodeBase.Core.ValueObjects
{
    public class ResponseContext<T> : ResponseExceptionContext<T> where T : IResponseData
    {
        public T Data { get; set; }
        public bool IsError { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ResponseContext()
        {
            
        }

        public ResponseContext(T data)
        {
            Data = data;
        }
    }

    public class ResponseExceptionContext<T> where T : IResponseData
    {
        public ExceptionResponse Error { get; set; }

        public ResponseExceptionContext()
        {

        }

        public ResponseExceptionContext(ExceptionResponse error)
        {
            Error = error;
        }
    }
}
