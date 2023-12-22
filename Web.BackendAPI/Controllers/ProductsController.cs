using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;

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
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProductCreateDto newProduct)
        {
            return Ok(await _productService.CreateAsync(newProduct));
        }
    }
}