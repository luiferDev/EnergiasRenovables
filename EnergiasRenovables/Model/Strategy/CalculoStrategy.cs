using EnergiasRenovables.Model.DTO;

namespace EnergiasRenovables.Model.Strategy
{
    public interface ICalculoStrategy<T, in TU>
    {
        List<T> ObtenerEnergia();
        decimal CalcularProduccion();
        Task AgregarEntidadConRelacionesAsync(TU entidadDto);
        Task ActualizarEntidadConRelacionesAsync(TU entidadDto, int id);
        Task EliminarEntidadConRelacionesAsync(int id);
    }
}
