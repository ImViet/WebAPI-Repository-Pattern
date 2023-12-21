using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.ProductDtos;

namespace Web.BackendAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProductCreateDto newProduct)
        {
            return Ok(await _productService.CreateAsync(newProduct));
        }
    }
}