using CodeBase.Core.Entities;
using CodeBase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbFactory _factory;
        private readonly DbSet<T> _table;

        private ApplicationDbContext _context;
        protected ApplicationDbContext Context => _context ?? (_context = _factory.Init());

        public EfRepository(IDbFactory factory)
        {
            _factory = factory;
            _table = Context.Set<T>();
        }

        public T Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            return _table.Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public T Delete(int id, bool soft = true)
        {
            var entity = _table.Find(id);

            if (soft)
            {
                entity.DeletedAt = DateTime.Now;
                Update(entity, new List<Expression<Func<T, object>>>
                {
                    x => x.DeletedAt
                });
            }
            else
            {
                // this entity is found from the db directly
                // so no need to attach it again
                _table.Remove(entity);
            }

            return entity;
        }

        public T Delete(T entity, bool soft = true)
        {
            if (soft)
            {
                entity.DeletedAt = DateTime.Now;
                Update(entity, new List<Expression<Func<T, object>>>
                {
                    x => x.DeletedAt
                });
            }
            else
            {
                // because the passed in entity may not tracking
                // so attach it to the table to be able to delete it
                if (IsDetached(entity))
                    _table.Attach(entity);

                _table.Remove(entity);
            }

            return entity;
        }

        public void Delete(ISpecification<T> spec, bool soft = true)
        {
            var entities = ApplySpecification(spec);

            if (soft)
            {
                // You have to inherit the base repository and implement the batch delete on each entities
                throw new NotSupportedException($"Entity of type {typeof(T)} does not support batch delete yet!");
            }
            else
            {
                _table.RemoveRange(entities);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null)
        {
            if (updateProperties == null)
                updateProperties = new List<Expression<Func<T, object>>>();

            // entity must be attached before update any properties
            if (IsDetached(entity)) _table.Attach(entity);

            entity.UpdatedAt = DateTime.Now;
            updateProperties.Add(x => x.UpdatedAt);

            if (updateProperties?.Count > 0)
            {
                updateProperties.ForEach(p => _context.Entry(entity).Property(p).IsModified = true);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            return entity;
        }

        private bool IsDetached(T entity)
            => Context.Entry(entity).State == EntityState.Detached;

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            => SpecificationEvaluator<T>.GetQuery(_table.AsQueryable(), spec);
    }
}
