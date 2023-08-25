using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Contracts.Dtos.UserDtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
