using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;

namespace Web.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
