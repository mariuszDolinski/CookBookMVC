using FluentValidation;

namespace CookBook.Application.UnitUtils.Commands.EditUnit
{
    public class EditUnitCommandValidator : AbstractValidator<EditUnitCommand>
    {
        public EditUnitCommandValidator()
        {
            RuleFor(ing => ing.Name)
                .NotEmpty().WithMessage("Podaj nazwę składnika")
                .MinimumLength(3).WithMessage("Nazwa powinna mieć minimum 3 znaki")
                .MaximumLength(50).WithMessage("Nazwa powinna mieć maksimum 50 znaków");
        }
    }
}
