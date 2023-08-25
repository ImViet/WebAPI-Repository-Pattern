using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.UserDtos;

namespace Web.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm]UserRegisterDto userRegister)
        {
            var user = await _userService.AddAsync(userRegister);
            if (user == null)
            {
                return BadRequest("Cannot create user");
            }
            return Ok(user);
        }
    }
}
