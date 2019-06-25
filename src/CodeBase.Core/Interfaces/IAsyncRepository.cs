using System;
using CodeBase.Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeBase.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, List<Expression<Func<T, object>>> updateProperties = null);
        Task DeleteAsync(object id, bool soft = true);
        Task DeleteAsync(Expression<Func<T, bool>> where, bool soft = true);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
