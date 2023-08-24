using Microsoft.AspNetCore.Identity;
using Web.DataAccessor.Data;
using Web.DataAccessor.Entities;

namespace Web.BackendAPI.Extensions
{
    public static class AuthenticationRegister
    {
        public static void AddAuthenticationRegister(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
