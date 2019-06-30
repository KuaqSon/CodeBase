using System.Data.Entity;
using entity = CodeBase.Core.Entities;

namespace CodeBase.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<entity.Event> Events { get; set; }

        public ApplicationDbContext() : base("ApplicationConnection")
        {
            this.Database.Log = msg => System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
