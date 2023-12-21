using Microsoft.AspNetCore.Http;

namespace Web.Contracts.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }

        public List<int>? CategoriesId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}