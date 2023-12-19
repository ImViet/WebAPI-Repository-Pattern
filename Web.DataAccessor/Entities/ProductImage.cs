namespace Web.DataAccessor.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Caption { get; set; }
        public string? ImagePath { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public Product Product { get; set; }

    }
}