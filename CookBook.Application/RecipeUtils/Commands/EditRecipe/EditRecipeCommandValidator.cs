using FluentValidation;

namespace CookBook.Application.RecipeUtils.Commands.EditRecipe
{
    public class EditRecipeCommandValidator : AbstractValidator<EditRecipeCommand>
    {
        public EditRecipeCommandValidator() 
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Podaj nazwę przepisu")
                .MinimumLength(5).WithMessage("Nazwa powinna mieć minimum 5 znaków")
                .MaximumLength(100).WithMessage("Nazwa powinna mieć maksimum 100 znaków");
        }
    }
}
