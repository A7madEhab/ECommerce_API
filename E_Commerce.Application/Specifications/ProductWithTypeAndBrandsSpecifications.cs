using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandsSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithTypeAndBrandsSpecifications()
        {
                AddInclude(p=>p.ProductType);
                AddInclude(p => p.ProductBrand);
        }
    }
}
