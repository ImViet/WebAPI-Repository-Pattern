using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;
using Web.Contracts.Models;

namespace Web.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<CommandResultModel<List<CategoryDto>>> GetAllAsync();
        Task<CommandResultModel<PagedResponseModel<CategoryDto>>> GetPagingAsync(CategoryQueryDto query);
        Task<CommandResultModel<CategoryDto>> GetByIdAsync(int id);
        Task<CommandResultModel<PagedResponseModel<ProductDto>>> GetProductByCategoryAsync(int categoryId, ProductQueryDto query);
        Task<CommandResultModel<bool>> CreateAsync(CategoryCreateDto newCategory);
        Task<CommandResultModel<bool>> UpdateAsync(CategoryUpdateDto updateCategory);
        Task<CommandResultModel<bool>> DeleteAsync(int id);
    }
}
