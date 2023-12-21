using Web.Contracts.Dtos.ImageDtos;

namespace Web.Contracts.Dtos.ProductDtos
{
    public class ProductDto
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}