using CookBook.Domain.Interfaces;
using FluentValidation;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeCategory
{
    public class CreateRecipeCategoryCommandValidator : AbstractValidator<CreateRecipeCategoryCommand>
    {
        public CreateRecipeCategoryCommandValidator(IRecipeCategoryRepository recipeCategoryRepository) 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Podaj nazwę kategorii")
                .MaximumLength(100).WithMessage("Nazwa powinna mieć maksimum 100 znaków")
                .Custom((value, context) =>
                {
                    if (value is not null)
                    {
                        var category = recipeCategoryRepository.GetByName(value).Result;
                        if (category is not null)
                        {
                            context.AddFailure($"Kategoria '{value}' już istnieje!");
                        }
                    }
                });
        }
    }
}
