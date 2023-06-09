﻿using FluentValidation;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipe
{
    public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator() 
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Podaj nazwę przepisu")
                .MinimumLength(5).WithMessage("Nazwa powinna mieć minimum 5 znaków")
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
