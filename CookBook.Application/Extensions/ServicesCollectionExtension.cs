using CookBook.Application.DtoModels.Validators;
using CookBook.Application.Mappings;
using CookBook.Application.Services;
using CookBook.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Application.Extensions
{
    public static class ServicesCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CookBookMappingProfile));
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IFileService, FileService>();
            services.AddValidatorsFromAssemblyContaining<RecipeDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
