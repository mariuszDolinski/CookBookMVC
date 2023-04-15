using FluentValidation;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipe
{
    internal class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator() 
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Podaj nazwę przepisu")
                .MinimumLength(3).WithMessage("Nazwa powinna mieć minimum 3 znaki")
                .MaximumLength(100).WithMessage("Nazwa powinna mieć maksimum 100 znaków");
            RuleFor(r => r.ImageFile)
                .Custom((value, context) =>
                {
                    if (value != null && !value.ContentType.Contains("image"))
                    {
                        context.AddFailure("Przesłany plik nie jest zdjęciem");
                    }
                });
        }
    }
}
