using CodeBase.Core.Services.Event;
using CodeBase.Core.ValueObjects;
using CodeBase.Core.ValueObjects.Events;
using System.Threading.Tasks;
using System.Web.Http;

namespace CodeBase.Api.Controllers
{
    [RoutePrefix("api/events")]
    public class EventApiController : BaseApiController
    {
        private readonly IEventService _eventService;

        public EventApiController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost, Route("add")]
        public async Task<ResponseContext<EventResponseData>> AddEventAsync(RequestContext<EventRequestData> request)
        {
            var context = await _eventService.AddEventAsync(request.Payload);

            return OkResponse(context);
        }
    }
}
