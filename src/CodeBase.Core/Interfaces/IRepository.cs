using System;
using CodeBase.Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeBase.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        T Add(T entity);
        T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null);
        T Delete(int id, bool soft = true);
        T Delete(T entity, bool soft = true);
        void Delete(ISpecification<T> spec, bool soft = true);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
