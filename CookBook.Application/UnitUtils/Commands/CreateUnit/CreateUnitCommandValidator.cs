using CookBook.Domain.Interfaces;
using FluentValidation;

namespace CookBook.Application.UnitUtils.Commands.CreateUnit
{
    public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
    {
        public CreateUnitCommandValidator(IUnitRepository unitRepository) 
        {
            RuleFor(unit => unit.Name)
                .NotEmpty().WithMessage("Podaj nazwę jednostki")
                .MinimumLength(3).WithMessage("Nazwa powinna mieć minimum 3 znaki")
                .MaximumLength(50).WithMessage("Nazwa powinna mieć maksimum 50 znaków")
                .Custom((value, context) =>
                {
                    if (value is not null)
                    {
                        var unit = unitRepository.GetByName(value).Result;
                        if (unit is not null)
                        {
                            context.AddFailure($"Jednostka '{value}' już istnieje!");
                        }
                    }
                });
        }
    }
}
