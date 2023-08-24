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
            var account = await _authenticationService.GetAccountByUserNameAsync(userLogin.UserName);
            if (account == null)
            {
                return BadRequest("User not found");
            }
            account = await _authenticationService.LoginAsync(userLogin);
            if(account == null)
            {
                return BadRequest("Username or Password is incorrect. Please try again.");
            }
            return Ok(account);
        }
        
    }
}
