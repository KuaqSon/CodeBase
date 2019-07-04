namespace CodeBase.Core.ValueObjects
{
    public class RequestContext<T> where T : IRequestData
    {
        public RequestContext()
        {
            
        }

        public RequestContext(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; set; }
        public string AuthToken { get; set; }
    }
}
