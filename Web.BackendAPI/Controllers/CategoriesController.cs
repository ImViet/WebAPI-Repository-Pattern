using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;

namespace Web.BackendAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }
        [HttpGet]
        [Route("paging")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPaging([FromQuery] CategoryQueryDto query)
        {
            return Ok(await _categoryService.GetPagingAsync(query));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }
        [HttpGet]
        [Route("{id}/products")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductsByCategory([FromQuery] ProductQueryDto query)
        {
            return Ok(await _categoryService.GetProductByCategoryAsync(query));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CategoryCreateDto newCategory)
        {
            return Ok(await _categoryService.CreateAsync(newCategory));
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromForm] CategoryUpdateDto updateCategory)
        {
            return Ok(await _categoryService.UpdateAsync(updateCategory));
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _categoryService.DeleteAsync(id));
        }
    }
}
