using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.UserDtos;

namespace Web.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AddAsync(UserRegisterDto userRegister);
    }
}
