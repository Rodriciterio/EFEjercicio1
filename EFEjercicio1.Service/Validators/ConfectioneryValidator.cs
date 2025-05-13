using EFEjercicio1Entities;
using FluentValidation;

namespace EFEjercicio1.Service.Validators
{
    public class ConfectioneryValidator : AbstractValidator<Confectionery>
    {
        public ConfectioneryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("El nombre es requerido")
                .MinimumLength(10).WithMessage("El nombre debe tener al menos 10 caracteres")
                .MaximumLength(50).WithMessage("El nombre no puede tener mas de 50 caracteres");
        }
    }
}
