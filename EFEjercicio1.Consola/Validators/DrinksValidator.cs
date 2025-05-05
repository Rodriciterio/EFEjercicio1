using EFEjercicio1Entities;
using FluentValidation;

namespace EFEjercicio1.Consola.Validators
{
    public class DrinksValidator : AbstractValidator<Drink>
    {
        public DrinksValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("El nombre es requerido")
                .MinimumLength(6).WithMessage("El nombre debe tener almenos de 6 caracteres")
                .MaximumLength(100).WithMessage("El nombre no puede tener mas de 100 caracteres");

            RuleFor(d => d.Size).NotEmpty().WithMessage("El nombre es requerido")
                .MinimumLength(5).WithMessage("El tamaño debe tener al menos 5 caracteres")
                .MaximumLength(20).WithMessage("El tamaño no puede tener mas de 20 caracteres");
        }
    }
}
