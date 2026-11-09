using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity:BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default);
        Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default);
        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default);
    }
}
