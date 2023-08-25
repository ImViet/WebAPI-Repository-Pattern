using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Contracts.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException()
        {

        }
        public BadRequestException(string message): base(message)
        {

        }
    }
}
