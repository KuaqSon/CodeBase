using CodeBase.Core.Values.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBase.Core.Services.Events
{
    public interface IEventServices
    {
        Task<List<EventResponseData>> GetAllEventsAsync(EventRequestData request);
        Task<EventResponseData> AddEventAsync(EventRequestData request);
    }
}
