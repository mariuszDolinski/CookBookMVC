using AutoMapper;
using CookBook.Application.ApplicationUser;
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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateRecipeCommand)));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CookBookMappingProfile(userContext));
            }).CreateMapper());

            services.AddValidatorsFromAssemblyContaining<CreateRecipeCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
