using CodeBase.Core.Entities;
using CodeBase.Core.Repositories.Events;

namespace CodeBase.Infrastructure.Data.Repositories
{
    public class EventRepository : EfRepository<Event>, IEventRepository
    {
        public EventRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}
