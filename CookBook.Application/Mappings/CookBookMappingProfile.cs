using AutoMapper;
using CookBook.Application.ApplicationUser;
using CookBook.Application.IngridientUtils;
using CookBook.Application.RecipeUtils;
using CookBook.Application.RecipeUtils.Commands.EditImage;
using CookBook.Application.RecipeUtils.Commands.EditRecipe;
using CookBook.Application.UnitUtils;
using CookBook.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Application.Mappings
{
    internal class CookBookMappingProfile : Profile
    {
        public CookBookMappingProfile(IUserContext userContext) 
        {
            var user = userContext.GetCurrentUser();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Recipe, RecipeDto>()
                .ForMember(r => r.CreatedTime, opt => opt.MapFrom(
                    src => src.CreatedTime.ToString("dd.MM.yyyy, HH:mm")))
                .ForMember(r => r.Author, opt => opt.MapFrom(
                    src => userContext.GetUserNameById(src.AuthorId).Result))
                .ForMember(r => r.IsEditable, opt => opt.MapFrom(src => 
                user != null && (user.IsInRole("Manager") || user.IsInRole("Admin") || src.AuthorId == user.Id)));
            CreateMap<Recipe, PreviewRecipeDto>();
            CreateMap<RecipeDto, EditRecipeCommand>();
            CreateMap<RecipeDto, EditImageCommand>();

            CreateMap<Ingridient, IngridientDto>()
                .ForMember(ing => ing.CreatedBy, opt => opt.MapFrom(
                    src => (src.CreatedById != null) ? userContext.GetUserNameById(src.CreatedById).Result : "brak"))
                .ForMember(ing => ing.CreatedTime, opt => opt.MapFrom(
                    src => src.CreatedTime.ToString("dd.MM.yyyy, HH:mm")));
            CreateMap<IngridientDto, Ingridient>();

            CreateMap<Unit, UnitDto>()
                .ForMember(ing => ing.CreatedBy, opt => opt.MapFrom(
                    src => (src.CreatedById != null) ? userContext.GetUserNameById(src.CreatedById).Result : "brak"))
                .ForMember(ing => ing.CreatedTime, opt => opt.MapFrom(
                    src => src.CreatedTime.ToString("dd.MM.yyyy, HH:mm")));
            CreateMap<UnitDto, Unit>();
        }
    }
}
