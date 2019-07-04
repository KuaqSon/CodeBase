using System.Collections.Generic;

namespace CodeBase.Core.ValueObjects
{
    public enum RequestMethod { Post, Patch, Put, Delete }
    public class RequestOption
    {
        public bool SkipAuth { get; set; }
        public RequestMethod Method { get; set; } = RequestMethod.Post;
        public Dictionary<string, object> RequestParams { get; set; }
    }
}
