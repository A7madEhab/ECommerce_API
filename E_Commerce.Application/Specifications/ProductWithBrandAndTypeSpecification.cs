using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product, int>
    {
        private readonly int? _id;

        // Constructor for getting a SINGLE product by ID (Where ID == id + Includes)
        public ProductWithBrandAndTypeSpecification(int id) : base()
        {
            _id = id;
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        // Constructor for getting ALL products (Includes only, no WHERE filter)
        public ProductWithBrandAndTypeSpecification() : base()
        {
            _id = null;
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        // Implement ToExpression based on whether an ID was provided
        public override Expression<Func<Product, bool>> ToExpression()
        {
            if (_id.HasValue)
            {
                return p => p.Id == _id.Value; // Translates to WHERE Id = @id
            }

            return null; // No WHERE filter needed when fetching all products
        }
    }
}