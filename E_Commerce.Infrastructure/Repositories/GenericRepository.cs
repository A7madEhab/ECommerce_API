using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEnitiy, Tkey> (StoreDbContext _storeDbContext): IGenericRepository<TEnitiy, Tkey> where TEnitiy : BaseEntity<Tkey>
    {
        public void Add(TEnitiy entity)=>_storeDbContext.Add(entity);

        public async Task<IReadOnlyList<TEnitiy>> GetAllAsync(CancellationToken cancellationToken = default)
        {
          return await  _storeDbContext.Set<TEnitiy>().ToListAsync(cancellationToken);
        }

        public async Task<TEnitiy?> GetByIdAsync(Tkey id, CancellationToken cancellationToken = default)
        {
            return await _storeDbContext.Set<TEnitiy>().FirstOrDefaultAsync(e => e.Id!.Equals(id),cancellationToken);
        }

        public void Remove(TEnitiy entity)
        {
          _storeDbContext.Remove(entity);
        }

        public void Update(TEnitiy entity)
        {
            _storeDbContext.Update(entity);
        }
    }
}
