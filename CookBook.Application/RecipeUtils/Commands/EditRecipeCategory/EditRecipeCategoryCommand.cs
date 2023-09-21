using CookBook.Application.Commons;
using MediatR;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipeCategory
{
    public class EditRecipeCategoryCommand: RecipeCategoryDto, IRequest<string>, IEditItemCommand
    {
        public string OldName { get; set; } = string.Empty;
    }
}
