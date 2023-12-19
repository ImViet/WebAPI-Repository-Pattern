using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DataAccessor.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductsInCategories> ProductsInCategories { get; set; }

    }
}
