﻿using CodeBase.Core.ValueObjects.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBase.Core.Services.Event
{
    public interface IEventService
    {
        Task<List<EventResponseData>> GetAllEventsAsync(EventRequestData request);
        Task<EventResponseData> AddEventAsync(EventRequestData request);
    }
}
