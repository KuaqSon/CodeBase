using CodeBase.Core.Entities;
using System.Threading.Tasks;

namespace CodeBase.Core.Interfaces
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IRepository<T> Get<T>() where T : BaseEntity;
    }
}
