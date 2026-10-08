using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Product: BaseEntity<int>
    {
        public string Description { get; set; }=string.Empty;
        public string pictureUrl { get; set; }=string.Empty;
        public decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;
        public int BrandId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}
