using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Product: BaseEntity<int>
    {
        public string Description { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [ForeignKey(nameof(BrandId))]
        public ProductBrand ProductBrand { get; set; } = null!;

        public int BrandId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public ProductType ProductType { get; set; } = null!;

        public int TypeId { get; set; }
    }
}
