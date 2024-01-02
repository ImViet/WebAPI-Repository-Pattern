using Web.Contracts.Dtos.ImageDtos;

namespace Web.Contracts.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}