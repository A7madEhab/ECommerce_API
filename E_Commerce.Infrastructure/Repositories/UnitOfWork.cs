using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext _storeContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> Repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            var typeName=typeof(TEntity).Name;
            if (Repositories.TryGetValue(typeName, out object? obj))
                return (IGenericRepository<TEntity, TKey>)obj;
            var repo=new GenericRepository<TEntity,TKey>(_storeContext);
            Repositories.Add(typeName, repo);
            return repo;
        }

        public  async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
          return await  _storeContext.SaveChangesAsync(ct);
        }
    }
}
