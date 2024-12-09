using EnergiasRenovables.Model.DTO;
using FluentValidation;

namespace EnergiasRenovables.Validations;

public class ValidationPlantaProduccion: AbstractValidator<AgregarPlantaProduccionDto>
{
    public ValidationPlantaProduccion()
    {
        RuleFor(x => x.Ubicacion)
            .NotEmpty().WithMessage("La ubicación no puede estar vacía")
            .MaximumLength(50).WithMessage("La ubicación no puede tener más de 50 caracteres");
        RuleFor(x => x.CapacidadInstalada)
            .NotEmpty().WithMessage("La capacidad no puede estar vacía")
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0");
        RuleFor(x => x.Eficiencia)
            .NotEmpty().WithMessage("La eficiencia no puede estar vacía")
            .InclusiveBetween(0.0m, 1.0m).WithMessage("El valor debe estar entre 0.0 y 1.0 como porcentaje.");
        RuleFor(x => x.FechaCreacion)
            .NotEmpty().WithMessage("La fecha de creación no puede estar vacía")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("La fecha de creación no puede ser mayor a la fecha actual");
    }
}