using CodeBase.Core.Entities;
using CodeBase.Core.Interfaces;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _factory;
        private ApplicationDbContext _context;
        public ApplicationDbContext Context => _context ?? (_context = _factory.Init());

        public UnitOfWork(IDbFactory factory)
        {
            _factory = factory;
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public IRepository<T> Get<T>() where T : BaseEntity
        {
            return new EfRepository<T>(_factory);
        }
    }
}
