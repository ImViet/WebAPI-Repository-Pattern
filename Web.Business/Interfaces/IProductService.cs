using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;
using Web.Contracts.Models;

namespace Web.Business.Interfaces
{
    public interface IProductService
    {
        Task<CommandResultModel<List<ProductDto>>> GetAllAsync();
        Task<CommandResultModel<PagedResponseModel<ProductDto>>> GetProductAsync(ProductQueryDto query);
        Task<CommandResultModel<ProductDto>> GetByIdAsync(int id);
        Task<CommandResultModel<bool>> CreateAsync(ProductCreateDto newProduct);
    }
}