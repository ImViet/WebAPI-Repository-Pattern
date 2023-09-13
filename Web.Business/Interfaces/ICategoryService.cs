using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;

namespace Web.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<List<CategoryDto>> GetPagingAsync(CategoryQueryDto query);
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto newCategory);
        Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateCategory);
        Task<bool> DeleteAsync(int id);

        //Test with Store procedure
        Task<List<CategoryDto>> GetAllWithSPAsync();
    }
}
