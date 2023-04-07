using FluentValidation;
using static System.Net.Mime.MediaTypeNames;

namespace CookBook.Application.DtoModels.Validators
{
    public class RecipeDtoValidator : AbstractValidator<RecipeDto>
    {
        public RecipeDtoValidator() 
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
