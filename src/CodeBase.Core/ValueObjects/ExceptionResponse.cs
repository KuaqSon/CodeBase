using System;

namespace CodeBase.Core.ValueObjects
{
    public class ExceptionResponse : IResponseData
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Uri RequestUri { get; set; }
    }
}
