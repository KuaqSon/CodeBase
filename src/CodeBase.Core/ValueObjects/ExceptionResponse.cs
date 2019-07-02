using System;

namespace CodeBase.Core.ValueObjects
{
    public class ExceptionResponse : BaseResponseData
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Uri RequestUri { get; set; }
    }
}
