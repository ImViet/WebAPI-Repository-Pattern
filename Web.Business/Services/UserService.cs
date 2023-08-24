using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.UserDtos;
using Web.Contracts.Exceptions;
using Web.DataAccessor.Entities;

namespace Web.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDto> AddAsync(UserRegisterDto userRegister)
        {
            var userName = await _userManager.FindByNameAsync(userRegister.UserName);
            if(userName != null)
            {
                throw new BadRequestException("Username is already");
            }
            var email = await _userManager.FindByEmailAsync(userRegister.Email);
            if (email != null)
            {
                throw new BadRequestException("Email is already");
            }
            var user = _mapper.Map<User>(userRegister);
            user.Id = Guid.NewGuid();
            user.DateCreated = DateTime.Now;
            var result = await _userManager.CreateAsync(user, userRegister.Password);
            if(!result.Succeeded)
            {
                throw new ErrorException("Cannot create user. Something went wrong!!!");
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
