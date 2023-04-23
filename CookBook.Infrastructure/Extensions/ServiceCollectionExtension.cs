using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using CookBook.Infrastructure.Repositories;
using CookBook.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<CookBookDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CookBook")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Stores.MaxLengthForKeys = 450;
            })
                .AddEntityFrameworkStores<CookBookDbContext>()
                .AddErrorDescriber<CustomIdentityErrorMessages>();

            services.AddScoped<CookBookSeeder>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IIngridientRepository, IngridientRepository>();
        }
    }
}
