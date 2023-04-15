using AutoMapper;
using CookBook.Application.RecipeUtils;
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
        }
    }
}
