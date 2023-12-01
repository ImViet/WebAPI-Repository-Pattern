using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DataAccessor.Configurations;
using Web.DataAccessor.Entities;
using Web.DataAccessor.Entities.StoreProcedureResult;

namespace Web.DataAccessor.Data
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        //Store procedure result
        public DbSet<Sp_GetAllCategory_Result> Sp_GetAllCategory_Results { get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            //Config table from identity db context
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");

            //Config test store procedure
            modelBuilder.Entity<Sp_GetAllCategory_Result>().HasNoKey();
        }
    }
}
