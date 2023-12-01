using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.UserDtos;
using Web.Contracts.Exceptions;

namespace Web.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDto userLogin)
        {
            return Ok(await _authenticationService.LoginAsync(userLogin));
        }
        
    }
}
