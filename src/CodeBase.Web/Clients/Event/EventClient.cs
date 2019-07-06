using CodeBase.Core.Commons;
using CodeBase.Core.ValueObjects;
using CodeBase.Core.ValueObjects.Events;
using CodeBase.Infrastructure.Internet;
using System.Threading.Tasks;

namespace CodeBase.Web.Clients.Event
{
    public class EventClient : BaseHttpClient, IEventClient
    {
        public EventClient(IConfiguration config) : base(config)
        {
        }

        protected override string RoutePrefix => "api/events/";

        public async Task<ResponseContext<EventResponseData>> AddEventAsync(RequestContext<EventRequestData> request)
        {
            return await PostAsync<EventRequestData, EventResponseData>("add", request);
        }
    }
}