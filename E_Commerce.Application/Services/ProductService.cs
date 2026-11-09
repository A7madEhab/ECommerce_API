using AutoMapper;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Shared;
using E_Commerce.Application.Specifications;
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
            // Use Specification to ensure Brand and Type are JOINed and loaded
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdWithSpecAsync(spec, ct);

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
            // Use Specification to include Brand and Type for all products
            var spec = new ProductWithBrandAndTypeSpecification();
            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec, ct);

            var result = mapper.Map<IReadOnlyList<ProductDTO>>(products);

            return Result.Success(result);
        }

        public async Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            // Brands and Types don't have nested relations, so standard GetAll is fine here
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