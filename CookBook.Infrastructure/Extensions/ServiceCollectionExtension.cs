﻿using CookBook.Domain.Entities;
using CookBook.Domain.Interfaces;
using CookBook.Infrastructure.CommonServices;
using CookBook.Infrastructure.Persistence;
using CookBook.Infrastructure.Repositories;
using CookBook.Infrastructure.Seeders;
using CookBook.Infrastructure.Utilities;
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

            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Stores.MaxLengthForKeys = 450;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CookBookDbContext>()
                .AddErrorDescriber<CustomIdentityErrorMessages>();

            services.AddScoped<CookBookSeeder>();
            services.AddScoped<IRepositoryUtils, RepositoryUtils>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IIngridientRepository, IngridientRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IRecipeIngridientRepository, RecipeIngridientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecipeCategoryRepository, RecipeCategoryRepository>();
            services.AddScoped<ICommonService<Ingridient>, CommonService<Ingridient>>();
            services.AddScoped<ICommonService<Unit>, CommonService<Unit>>();
            services.AddScoped<ICommonService<RecipeCategory>, CommonService<RecipeCategory>>();
            services.AddScoped<ICommonService<IngridientCategory>, CommonService<IngridientCategory>>();
            services.AddScoped<IIngridientCategoryRepository, IngridientCategoryRepository>();
        }
    }
}
