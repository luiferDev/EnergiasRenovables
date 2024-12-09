using EnergiasRenovables.Model.DTO;
using FluentValidation;

namespace EnergiasRenovables.Validations;

public class ValidationEnergiaSolar: AbstractValidator<AgregarEnergiaSolarDto> 
{
    public ValidationEnergiaSolar()
    {
        RuleFor(x => x.EficienciaPaneles)
            .NotNull().WithMessage("El campo seleccionado no puede ser nulo.")
            .InclusiveBetween(0.0m, 1.0m).WithMessage("El valor debe estar entre 0.0 y 1.0 como porcentaje.");
        RuleFor(x => x.AreaPaneles)
            .NotNull().WithMessage("El campo seleccionado no puede ser nulo.")
            .GreaterThan(0).WithMessage("El valor debe ser mayor que 0.");
        RuleFor(x => x.RadiacionSolar)
            .NotNull().WithMessage("El campo seleccionado no puede ser nulo.")
            .GreaterThan(0).WithMessage("El valor debe ser mayor que 0.");
        RuleFor(x => x.AnguloInclinacion)
            .NotNull().WithMessage("El campo seleccionado no puede ser nulo.")
            .InclusiveBetween(0, 90).WithMessage("El valor debe estar entre 0 y 90 grados.");
    }
}