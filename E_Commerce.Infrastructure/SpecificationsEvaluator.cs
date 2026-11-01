using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specs) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery.AsQueryable();
            if (specs.IncludeExpresstions.Any())
            {
                query = specs.IncludeExpresstions.Aggregate(query, (current, nextExp) => current.Include(nextExp));
            }
            

            return query;
        }

    }
}
