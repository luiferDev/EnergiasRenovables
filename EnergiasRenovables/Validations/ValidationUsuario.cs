using EnergiasRenovables.Model.DTO;
using FluentValidation;

namespace EnergiasRenovables.Validations;

public class ValidationUsuario: AbstractValidator<UsuarioRegister>
{
    public ValidationUsuario()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres")
            .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres");
        RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
            .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
            .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
            .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial");
        RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es válido");
    }
}