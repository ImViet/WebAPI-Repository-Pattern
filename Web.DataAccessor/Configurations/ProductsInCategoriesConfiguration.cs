using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.DataAccessor.Entities;

namespace Web.DataAccessor.Configurations
{
    public class ProductsInCategoriesConfiguration : IEntityTypeConfiguration<ProductsInCategories>
    {
        public void Configure(EntityTypeBuilder<ProductsInCategories> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductsInCategories).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Category).WithMany(x => x.ProductsInCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}