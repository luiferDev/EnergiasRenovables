using EnergiasRenovables.Model.DTO;

namespace EnergiasRenovables.Model.Strategy
{
    public interface ICalculoStrategy<T, U>
    {
        List<T> ObtenerEnergia();
        decimal CalcularProduccion();
        Task AgregarEntidadConRelacionesAsync(U entidadDto);
        Task ActualizarEntidadConRelacionesAsync(U entidadDto, int id);
        Task EliminarEntidadConRelacionesAsync(int id);
    }
}
