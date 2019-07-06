using CodeBase.Core.ValueObjects;
using CodeBase.Core.ValueObjects.Events;
using System.Threading.Tasks;

namespace CodeBase.Web.Clients.Event
{
    public interface IEventClient
    {
        Task<ResponseContext<EventResponseData>> AddEventAsync(RequestContext<EventRequestData> request);
    }
}