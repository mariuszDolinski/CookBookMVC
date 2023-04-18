using FluentValidation;

namespace CookBook.Application.RecipeUtils.Commands.EditImage
{
    public class EditImageCommandValidator : AbstractValidator<EditImageCommand>
    {
        public EditImageCommandValidator() 
        {
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
