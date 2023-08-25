using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Contracts.Dtos.AuthDtos
{
    public class AccountDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
