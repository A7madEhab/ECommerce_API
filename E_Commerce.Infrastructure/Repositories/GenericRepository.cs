using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Specefications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
            where TEntity : BaseEntity<TKey>
    {
        protected readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Set<TEntity>().ToListAsync(ct);
        }

        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            // Pass cancellation token to FindAsync
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default)
        {
            return await ApplySpecification(spec).ToListAsync(ct);
        }

        public async Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(ct);
        }

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default)
        {
            return await ApplySpecification(spec).CountAsync(ct);
        }

        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _context.Set<TEntity>().AddAsync(entity, ct);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        // Private helper to wrap the query evaluation logic with specifications
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec);
        }
    }
}
