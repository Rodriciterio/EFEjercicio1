using EFEjercicio1Entities;
using FluentValidation;

namespace EFEjercicio1.Consola.Validators
{
    public class DrinksValidator : AbstractValidator<Drink>
    {
        public DrinksValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("The {PropertyName} is required")
                .MaximumLength(100).WithMessage("The {PropertyName} must have no more than {ComparisonValue} characters");

            RuleFor(d => d.Size).NotEmpty().WithMessage("The {PropertyName} is required")
                .MaximumLength(50).WithMessage("The {PropertyName} must have no more than {ComparisonValue} characters");
        }
    }
}
