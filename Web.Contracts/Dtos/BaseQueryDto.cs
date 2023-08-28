using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Contracts.Dtos
{
    public class BaseQueryDto
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public string Search { get; set; } = string.Empty;
    }
}
