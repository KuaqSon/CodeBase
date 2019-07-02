using System.Net;

namespace CodeBase.Core.ValueObjects
{
    public class ResponseContext<T> where T : BaseResponseData
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
}
