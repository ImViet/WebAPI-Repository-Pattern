using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;
using Web.Contracts.Models;

namespace Web.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<CommandResultModel<List<CategoryDto>>> GetAllAsync();
        Task<CommandResultModel<List<CategoryDto>>> GetPagingAsync(CategoryQueryDto query);
        Task<CommandResultModel<CategoryDto>> GetByIdAsync(int id);
        Task<CommandResultModel<bool>> CreateAsync(CategoryCreateDto newCategory);
        Task<CommandResultModel<bool>> UpdateAsync(CategoryUpdateDto updateCategory);
        Task<CommandResultModel<bool>> DeleteAsync(int id);
    }
}
