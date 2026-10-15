using AutoMapper;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Shared;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<ProductDTO>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id, ct);

            // 1. Guard against non-existent entities
            if (product is null)
            {
                return Error.NotFound(
                    "Product.NotFound",
                    $"Product with ID {id} was not found."
                );
            }

            var result = mapper.Map<ProductDTO>(product);
            return Result.Success(result);
        }

        public async Task<Result<IReadOnlyList<ProductDTO>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(ct);
            var result = mapper.Map<IReadOnlyList<ProductDTO>>(products);

            // Returning an empty list on Success (HTTP 200) is standard REST practice
            return Result.Success(result);
        }

        public async Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            var result = mapper.Map<IReadOnlyList<BrandDTO>>(brands);

            return Result.Success(result);
        }

        public async Task<Result<IReadOnlyList<TypeDTO>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);
            var result = mapper.Map<IReadOnlyList<TypeDTO>>(types);

            return Result.Success(result);
        }
    }
}