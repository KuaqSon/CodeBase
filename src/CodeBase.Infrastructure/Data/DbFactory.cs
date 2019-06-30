using CodeBase.Core.Commons;

namespace CodeBase.Infrastructure.Data
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ApplicationDbContext _context;

        public ApplicationDbContext Init()
        {
            return _context ?? (_context = new ApplicationDbContext());
        }

        protected override void DisposeCore()
        {
            _context?.Dispose();
            base.DisposeCore();
        }
    }
}
