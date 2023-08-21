using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DataAccessor.Entities;

namespace Web.DataAccessor.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
