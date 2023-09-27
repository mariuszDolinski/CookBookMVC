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
                    src => (src.CreatedById != null) ? userContext.GetUserNameById(src.CreatedById).Result : "-"))
                .ForMember(ing => ing.CreatedTime, opt => opt.MapFrom(
                    src => src.CreatedTime.ToString("dd.MM.yyyy, HH:mm")))
                .ForMember(ing => ing.IsEditable, opt => opt.MapFrom(src =>
                    user != null && (user.IsInRole("Admin") || user.IsInRole("Manager"))));
            CreateMap<IngridientDto, Ingridient>();

            CreateMap<Unit, UnitDto>()
                .ForMember(u => u.CreatedBy, opt => opt.MapFrom(
                    src => (src.CreatedById != null) ? userContext.GetUserNameById(src.CreatedById).Result : "-"))
                .ForMember(u => u.CreatedTime, opt => opt.MapFrom(
                    src => src.CreatedTime.ToString("dd.MM.yyyy, HH:mm")))
                .ForMember(u => u.IsEditable, opt => opt.MapFrom(src =>
                    user != null && (user.IsInRole("Admin") || user.IsInRole("Manager"))));
            CreateMap<UnitDto, Unit>();

            CreateMap<RecipeIngridient, RecipeIngridientDto>()
                .ForMember(ri => ri.Ingridient, opt => opt.Ignore())
                .ForMember(ri => ri.Unit, opt => opt.Ignore());

            CreateMap<RecipeCategory, RecipeCategoryDto>()
                .ForMember(rc => rc.Name, opt => opt.MapFrom(
                    src => src.Name))
                .ForMember(u => u.CreatedBy, opt => opt.MapFrom(
                    src => (src.CreatedById != null) ? userContext.GetUserNameById(src.CreatedById).Result : "-"))
                .ForMember(u => u.CreatedTime, opt => opt.MapFrom(
                    src => src.CreatedTime.ToString("dd.MM.yyyy, HH:mm")))
                .ForMember(u => u.IsEditable, opt => opt.MapFrom(src =>
                    user != null && (user.IsInRole("Admin") || user.IsInRole("Manager"))));

            CreateMap<RecipeCategoryDto, RecipeCategory>()
                .ForMember(rc => rc.Name, opt => opt.MapFrom(
                    src => src.Name));

            CreateMap<AppUser, UserDto>()
                .ForMember(u => u.UserRoles, opt => opt.Ignore());
        }
    }
}
