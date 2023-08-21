using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DataAccessor.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
