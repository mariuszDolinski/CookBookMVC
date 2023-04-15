using CookBook.Application.Mappings;
using CookBook.Application.RecipeUtils.Commands.CreateRecipe;
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
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateRecipeCommand)));
            services.AddAutoMapper(typeof(CookBookMappingProfile));
            services.AddScoped<IFileService, FileService>();
            services.AddValidatorsFromAssemblyContaining<CreateRecipeCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
