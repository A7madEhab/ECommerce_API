using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey>
            where TEntity : BaseEntity<TKey>
    {
        // Caching field to prevent expensive repeated compilation (.Compile())
        private Func<TEntity, bool>? _compiledExpression;

        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity)
        {
            // Compile only once and cache the delegate
            _compiledExpression ??= ToExpression().Compile();
            return _compiledExpression(entity);
        }

        // Helper method to make adding includes clean in concrete specifications
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}