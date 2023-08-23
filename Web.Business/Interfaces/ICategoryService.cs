using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.CategoryDtos;

namespace Web.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> CreateAsync(CategoryCreateDto newCategory);
        Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateCategory);
        Task<bool> DeleteAsync(int id);
    }
}
