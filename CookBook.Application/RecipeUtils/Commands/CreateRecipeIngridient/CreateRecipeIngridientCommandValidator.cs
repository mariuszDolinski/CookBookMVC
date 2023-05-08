using CookBook.Domain.Interfaces;
using FluentValidation;

namespace CookBook.Application.RecipeUtils.Commands.CreateRecipeIngridient
{
    public class CreateRecipeIngridientCommandValidator : AbstractValidator<CreateRecipeIngridientCommand>
    {
        public CreateRecipeIngridientCommandValidator(IIngridientRepository ingridientRepository, IUnitRepository unitRepository)
        {
            RuleFor(ri => ri.Amount)
                .NotEmpty().WithMessage("Podaj ilość (jako liczbę lub ułamek)")
                .Matches(@"^(\d+(?:[\.\,]\d*)?)$").WithMessage("Podaj poprawną liczbę lub ułamek dzisiętny");
            RuleFor(ri => ri.Ingridient)
                .NotEmpty().WithMessage("Wybierz składnik z listy")
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var ingridient = ingridientRepository.GetByName(value).Result;
                        if (ingridient == null)
                        {
                            context.AddFailure("Podany składnik nie istnieje. Wybierz składnik z listy lub dodaj nową.");
                        }
                    }
                });
            RuleFor(ri => ri.Unit)
               .NotEmpty().WithMessage("Wybierz jednostkę z listy")
               .Custom((value, context) =>
               {
                   if (value != null)
                   {
                       var unit = unitRepository.GetByName(value).Result;
                       if (unit == null)
                       {
                           context.AddFailure("Podana jednostka nie istnieje. Wybierz jednostkę z listy lub dodaj nową.");
                       }
                   }
               });
            RuleFor(ri => ri.Description)
                .NotEmpty().WithMessage("Podaj opis składnika");
        }
    }
}
