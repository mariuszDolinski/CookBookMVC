using AutoMapper;
using CookBook.Application.IngridientUtils;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.EditImage;
using CookBook.Application.RecipeUtils.Commands.EditRecipe;
using CookBook.Domain.Entities;

namespace CookBook.Application.Mappings
{
    internal class CookBookMappingProfile : Profile
    {
        public CookBookMappingProfile() 
        {
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Recipe, PreviewRecipeDto>();
            CreateMap<Ingridient, IngridientDto>();
            CreateMap<RecipeDto, EditRecipeCommand>();
            CreateMap<RecipeDto, EditImageCommand>();
        }
    }
}
