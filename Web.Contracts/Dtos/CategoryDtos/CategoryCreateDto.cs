using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Contracts.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }
}
