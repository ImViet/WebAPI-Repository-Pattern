using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DataAccessor.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }

    }
}
