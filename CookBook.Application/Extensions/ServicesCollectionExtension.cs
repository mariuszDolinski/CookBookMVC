using CookBook.Application.Mappings;
using CookBook.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Application.Extensions
{
    public static class ServicesCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CookBookMappingProfile));
            services.AddScoped<IRecipeService, RecipeService>();
        }
    }
}
