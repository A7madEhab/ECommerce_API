using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService productService;
            
        public ProductsController( IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<Result<IReadOnlyList<ProductDTO>>>> GetAllProducts(CancellationToken ct)
        {
            var res = await productService.GetAllProductsAsync(ct);
            return Ok(res);
        }
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<Result<IReadOnlyList<ProductDTO>>>> GetAllBrands(CancellationToken ct)
        {
            var res= await productService.GetAllBrandsAsync(ct);
            return Ok(res);
        }
        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<Result<IReadOnlyList<TypeDTO>>>> GetAllTypes(CancellationToken ct)
        {
            var res = await productService.GetAllTypesAsync(ct);
            return Ok(res);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken ct)
        {
            var result = await productService.GetProductByIdAsync(id,ct);
            return HandleResult(result);
        }
    }
}
