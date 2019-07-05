using CodeBase.Core.Interfaces;
using CodeBase.Core.Repositories.Events;
using CodeBase.Core.ValueObjects.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBase.Core.Services.Event
{
    public class EventService : BaseService, IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepository _eventRepository;

        public EventService(IUnitOfWork unitOfWork, IEventRepository eventRepository)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;
        }

        public async Task<EventResponseData> AddEventAsync(EventRequestData request)
        {
            _eventRepository.Add(new Entities.Event());
            await _unitOfWork.CommitAsync();

            throw new NotImplementedException();
        }

        public Task<List<EventResponseData>> GetAllEventsAsync(EventRequestData request)
        {
            throw new NotImplementedException();
        }
    }
}
