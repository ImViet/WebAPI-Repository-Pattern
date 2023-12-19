using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;

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
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }
        [HttpGet]
        [Route("get-paging")]
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
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CategoryCreateDto newCategory)
        {
            return Ok(await _categoryService.CreateAsync(newCategory));
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromForm] CategoryUpdateDto updateCategory)
        {
            return Ok(await _categoryService.UpdateAsync(updateCategory));
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _categoryService.DeleteAsync(id));
        }
    }
}
