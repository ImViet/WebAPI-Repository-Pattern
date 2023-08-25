using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Business.Interfaces;
using Web.Contracts.Constants;
using Web.Contracts.Dtos.AuthDtos;
using Web.Contracts.Dtos.UserDtos;
using Web.Contracts.Exceptions;
using Web.DataAccessor.Entities;

namespace Web.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountByUserNameAsync(string userName)
        {
            var account = _mapper.Map<AccountDto>(await _userManager.FindByNameAsync(userName));
            if (account == null)
            {
                return new AccountDto();
            }
            return account;
        }

        public async Task<AccountDto> LoginAsync(UserLoginDto userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, true, true);
            if(!result.Succeeded)
            {
                throw new NotFoundException("User or Password is incorrect");
            }
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            string token = CreateToken(user);
            var account = _mapper.Map<AccountDto>(user);
            account.Token = token;
            return account;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                JWTSettings.Issuer,
                JWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
