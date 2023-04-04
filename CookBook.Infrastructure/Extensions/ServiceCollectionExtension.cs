using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.Persistence;
using CookBook.Infrastructure.Repositories;
using CookBook.Infrastructure.Seeders;
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

            services.AddScoped<CookBookSeeder>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
        }
    }
}
