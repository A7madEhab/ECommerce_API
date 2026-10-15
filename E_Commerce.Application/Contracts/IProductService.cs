using E_Commerce.Application.DTOs;
using E_Commerce.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDTO>>> GetAllProductsAsync(CancellationToken ct=default);
        Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct=default);
        Task<Result<IReadOnlyList<TypeDTO>>> GetAllTypesAsync(CancellationToken ct=default);
        Task<Result<ProductDTO>> GetProductByIdAsync(int id,CancellationToken ct=default);
    }
}
