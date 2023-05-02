using CookBook.Domain.Interfaces;
using FluentValidation;

namespace CookBook.Application.IngridientUtils.Commands.CreateIngridient
{
    public class CreateIngridientCommandValidator : AbstractValidator<CreateIngridientCommand>
    {
        public CreateIngridientCommandValidator(IIngridientRepository ingridientRepository)
        {
            RuleFor(ing => ing.Name)
                .NotEmpty().WithMessage("Podaj nazwę składnika")
                .MinimumLength(3).WithMessage("Nazwa powinna mieć minimum 3 znaki")
                .MaximumLength(50).WithMessage("Nazwa powinna mieć maksimum 50 znaków")
                .Custom((value, context) =>
                {
                    if(value is not null)
                    {
                        var ingridient = ingridientRepository.GetByName(value).Result;
                        if (ingridient is not null)
                        {
                            context.AddFailure($"Składnik '{value}' już istnieje!");
                        }
                    }
                });
        }
    }
}
