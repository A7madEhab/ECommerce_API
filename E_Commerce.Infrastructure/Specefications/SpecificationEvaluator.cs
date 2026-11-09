using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Infrastructure.Specefications
{
    public static class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery;

            // 1. Apply the WHERE clause if it exists
            if (spec.ToExpression() != null)
            {
                query = query.Where(spec.ToExpression());
            }

            // 2. Apply all INCLUDE expressions (JOINs)
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
