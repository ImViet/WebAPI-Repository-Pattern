using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;
using Web.Contracts.Models;

namespace Web.Business.Interfaces
{
    public interface IProductService
    {
        Task<CommandResultModel<PagedResponseModel<ProductDto>>> GetProductAsync(ProductQueryDto query);
    }
}